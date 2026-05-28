using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private readonly Dictionary<AudioEventData, int> activeCounts = new();

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

    public void Play(AudioEventData audioEvent)
    {
        if (audioEvent == null || audioEvent.clips.Length == 0)
            return;

        if (audioEvent.limit > 0 &&
            activeCounts.TryGetValue(audioEvent, out int count) &&
            count >= audioEvent.limit)
            return;

        StartCoroutine(PlayAudioEventRoutine(audioEvent));
    }

    public void Play(MultiSoundData multiSound)
    {
        if (multiSound == null)
            return;

        foreach (AudioEventData audioEvent in multiSound.audioEvents)
            Play(audioEvent);
    }

    private IEnumerator PlayAudioEventRoutine(AudioEventData audioEvent)
    {
        float delay = Random.Range(audioEvent.delay.x, audioEvent.delay.y);

        if (delay > 0f)
            yield return new WaitForSeconds(delay);

        AudioClip clip = audioEvent.clips[Random.Range(0, audioEvent.clips.Length)];

        GameObject audioObject = new GameObject("AudioEvent_" + audioEvent.name);

        AudioSource source = audioObject.AddComponent<AudioSource>();

        source.clip = clip;
        source.outputAudioMixerGroup = audioEvent.mixerGroup;

        source.volume = Mathf.Clamp01(audioEvent.volume + Random.Range(audioEvent.volumeShift.x, audioEvent.volumeShift.y));
        source.pitch = 1.0f + Random.Range(audioEvent.pitchShift.x, audioEvent.pitchShift.y);

        IncrementCount(audioEvent);

        source.Play();

        if (!source.loop)
        {
            yield return new WaitForSeconds(clip.length / Mathf.Abs(source.pitch));
            DecrementCount(audioEvent);
            Destroy(audioObject);
        }
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