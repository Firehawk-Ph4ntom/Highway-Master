using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    // Doesn't need to be serialized
    private class OwnedLoopData
    {
        public AudioSource source;
        public AudioEventData audioEvent;
    }

    public static AudioManager Instance;
    
    // Create Dictionaries to search for key value items needed for limit count and tracking Owner Objects for looping events
    private readonly Dictionary<AudioEventData, int> activeCounts = new();
    private readonly Dictionary<GameObject, List<OwnedLoopData>> ownedLoops = new();

    public static AudioManager Instance;

    // Since AudioManager is a singleton Object and must persist across all scenes, we use Awake() and DontDestroyOnLoad()
    private void Awake()
    {
        // Make sure only ONE AudioManager exists at any given moment
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Overload Functions, but they are distinguished by parameters, one for AudioEvents, one for MultiSounds
    // Function to play a single AudioEvent
    public void Play(AudioEventData audioEvent, GameObject owner = null)
    {
        if (!CanPlay(audioEvent))
            return;

        StartCoroutine(PlayAudioEvent(audioEvent, owner));
    }

    // Function to play a Multisound (a List of AudioEvents)
    public void Play(MultiSoundData multiSound, GameObject owner = null)
    {
        if (multiSound == null || multiSound.audioEvents == null)
            return;

        foreach (AudioEventData audioEvent in multiSound.audioEvents)
            Play(audioEvent, owner);
    }

    // Play an AudioEvent based on event metadata 
    private IEnumerator PlayAudioEvent(AudioEventData audioEvent, GameObject owner)
    {
        // Delay value is for when an AudioEvent shouldn't play immediately, but wait for a few seconds
        // Randomize Delay
        float delay = Random.Range(audioEvent.delay.x, audioEvent.delay.y);

        if (delay > 0.0f)
            yield return new WaitForSeconds(delay);

        // Every time an AudioEvent has to play, it needs to be tied to a GameObject Instance
        // If the AudioEvent isn't tied to anything, we create a new GameObject Clone (similar to Prefabs)
        // Then link the AudioEvent to that new GameObject with a new name
        GameObject audioObject = owner;

        if (audioObject == null)
            audioObject = new GameObject("AudioEvent_" + audioEvent.name);

        // Add Unity's AudioSource component to the AudioEvent Object
        AudioSource source = audioObject.AddComponent<AudioSource>();

        ApplyAudioEventToSource(audioEvent, source);
        source.loop = audioEvent.loop;

        // Handle limit counter
        IncrementCount(audioEvent);

        source.Play();

        // If the AudioEvent is set to loop = true, then it must continuously play
        if (audioEvent.loop)
        {
            RegisterOwnedLoop(owner, source, audioEvent);
            yield break;
        }

        yield return new WaitForSeconds(source.clip.length / Mathf.Abs(source.pitch));

        DecrementCount(audioEvent);

        // We need to do cleanups after AudioEvents finish playing, which, we need to destroy the GameObject clones
        // created, and the AudioSource as well
        if (owner == null)
            Destroy(audioObject);
        else
            Destroy(source);
    }

    // Called in AudioClientBehavior
    // Due to our Limit Count, if an AudioEvent is set to looping, cleanup doesn't occur cleanly 
    // (count doesn't decrement like normal non-looping events)
    // So, in order to do said cleanup, we need to destroy the AudioEvent alongside it's Owner Object
    public void StopOwnedAudio(GameObject owner)
    {
        if (owner == null)
            return;

        // If AudioEvent not looping, we don't need this function
        if (!ownedLoops.TryGetValue(owner, out List<OwnedLoopData> loops))
            return;

        // Loop over the AudioSource in the Dictionary
        foreach (OwnedLoopData loop in loops)
        {
            if (loop.source != null)
            {
                loop.source.Stop();
                Destroy(loop.source);
            }

            // In then end, since we destroyed the source, we can cleanly decrement Limit Count
            if (loop.audioEvent != null)
                DecrementCount(loop.audioEvent);
        }
        ownedLoops.Remove(owner);
    }

    // Register the GameObject that owns a looping AudioSource
    // Practical Dictionary usage goes here
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

    // The application of source to event happens here
    // Since we have the different metadata, but it doesn't do anything otherwise, we apply their usages here
    // Adjust Volume, Pitch, and assignment to SubMixSlider Group
    private void ApplyAudioEventToSource(AudioEventData audioEvent, AudioSource source)
    {
        AudioClip clip = audioEvent.clips[Random.Range(0, audioEvent.clips.Length)];

        source.clip = clip;
        source.outputAudioMixerGroup = audioEvent.mixerGroup;

        source.volume = Mathf.Clamp01(audioEvent.volume + Random.Range(audioEvent.volumeShift.x, audioEvent.volumeShift.y));
        source.pitch = 1.0f + Random.Range(audioEvent.pitchShift.x, audioEvent.pitchShift.y);
    }

    // Every AudioEvent has a Limit Count, and if Limit Count is exceeded, it should not play
    private bool CanPlay(AudioEventData audioEvent)
    {
        if (audioEvent == null || audioEvent.clips == null || audioEvent.clips.Length == 0)
            return false;

        if (audioEvent.limit > 0 && activeCounts.TryGetValue(audioEvent, out int count) && count >= audioEvent.limit)
            return false;

        return true;
    }

    // Whenever an AudioEvent is currently playing, increment Limit Count
    private void IncrementCount(AudioEventData audioEvent)
    {
        if (!activeCounts.ContainsKey(audioEvent))
            activeCounts[audioEvent] = 0;

        activeCounts[audioEvent]++;
    }

    // Whenever an AudioEvent has finished Playback, decrement Limit Count
    private void DecrementCount(AudioEventData audioEvent)
    {
        if (!activeCounts.ContainsKey(audioEvent))
            return;

        activeCounts[audioEvent]--;

        // Dictionary Cleanup if Limit Count is 0
        if (activeCounts[audioEvent] <= 0)
            activeCounts.Remove(audioEvent);
    }
}