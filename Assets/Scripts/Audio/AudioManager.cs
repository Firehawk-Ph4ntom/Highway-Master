using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    private class OwnedLoopData
    {
        public AudioSource source;
        public AudioEventData audioEvent;
    }
    
    public static AudioManager Instance;
    private readonly Dictionary<AudioEventData, int> activeCounts = new();
    private readonly Dictionary<GameObject, List<OwnedLoopData>> ownedLoops = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Play(AudioEventData audioEvent, GameObject owner = null)
    {
        if (!CanPlay(audioEvent))
            return;

        StartCoroutine(PlayAudioEvent(audioEvent, owner));
    }

    public void Play(MultiSoundData multiSound, GameObject owner = null)
    {
        if (multiSound == null || multiSound.audioEvents == null)
            return;

        foreach (AudioEventData audioEvent in multiSound.audioEvents)
            Play(audioEvent, owner);
    }

    private IEnumerator PlayAudioEvent(AudioEventData audioEvent, GameObject owner)
    {
        float delay = Random.Range(audioEvent.delay.x, audioEvent.delay.y);

        if (delay > 0.0f)
            yield return new WaitForSeconds(delay);

        GameObject audioObject = owner;

        if (audioObject == null)
            audioObject = new GameObject("AudioEvent_" + audioEvent.name);

        AudioSource source = audioObject.AddComponent<AudioSource>();

        ApplyAudioEventToSource(audioEvent, source);
        source.loop = audioEvent.loop;

        IncrementCount(audioEvent);
        source.Play();

        if (audioEvent.loop)
        {
            RegisterOwnedLoop(owner, source, audioEvent);
            yield break;
        }

        yield return new WaitForSeconds(source.clip.length / Mathf.Abs(source.pitch));

        DecrementCount(audioEvent);

        if (owner == null)
            Destroy(audioObject);
        else
            Destroy(source);
    }

    public void StopOwnedAudio(GameObject owner)
    {
        if (owner == null)
            return;

        if (!ownedLoops.TryGetValue(owner, out List<OwnedLoopData> loops))
            return;

        foreach (OwnedLoopData loop in loops)
        {
            if (loop.source != null)
            {
                loop.source.Stop();
                Destroy(loop.source);
            }

            if (loop.audioEvent != null)
                DecrementCount(loop.audioEvent);
        }

        ownedLoops.Remove(owner);
    }

    private void RegisterOwnedLoop(GameObject owner, AudioSource source, AudioEventData audioEvent)
    {
        if (owner == null || source == null || audioEvent == null)
            return;

        if (!ownedLoops.ContainsKey(owner))
            ownedLoops[owner] = new List<OwnedLoopData>();

        ownedLoops[owner].Add(new OwnedLoopData {
            source = source,
            audioEvent = audioEvent
        });
    }

    private void ApplyAudioEventToSource(AudioEventData audioEvent, AudioSource source)
    {
        AudioClip clip = audioEvent.clips[Random.Range(0, audioEvent.clips.Length)];

        source.clip = clip;
        source.outputAudioMixerGroup = audioEvent.mixerGroup;

        source.volume = Mathf.Clamp01(audioEvent.volume + Random.Range(audioEvent.volumeShift.x, audioEvent.volumeShift.y));
        source.pitch = 1.0f + Random.Range(audioEvent.pitchShift.x, audioEvent.pitchShift.y);
    }

    private bool CanPlay(AudioEventData audioEvent)
    {
        if (audioEvent == null || audioEvent.clips == null || audioEvent.clips.Length == 0)
            return false;

        if (audioEvent.limit > 0 &&
            activeCounts.TryGetValue(audioEvent, out int count) &&
            count >= audioEvent.limit)
            return false;

        return true;
    }

    private void IncrementCount(AudioEventData audioEvent)
    {
        if (!activeCounts.ContainsKey(audioEvent))
            activeCounts[audioEvent] = 0;

        activeCounts[audioEvent]++;
    }

    private void DecrementCount(AudioEventData audioEvent)
    {
        if (!activeCounts.ContainsKey(audioEvent))
            return;

        activeCounts[audioEvent]--;

        if (activeCounts[audioEvent] <= 0)
            activeCounts.Remove(audioEvent);
    }
}