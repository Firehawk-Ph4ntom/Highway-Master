using UnityEngine;

public class AudioLoopClientBehavior : MonoBehaviour
{
    public AudioEventData AudioEvent;
    private AudioSource source;

    private void Start()
    {
        // Get Audio Source component from Unity, and then force this audio to loop as long as the GameObject it
        // is attached to is alive
        source = GetComponent<AudioSource>();

        AudioClip clip = AudioEvent.clips[Random.Range(0, AudioEvent.clips.Length)];

        source.clip = clip;
        source.loop = true;
        source.outputAudioMixerGroup = AudioEvent.mixerGroup;

        // Set attributes to what's set in the AudioEvent metadata, acting like a child node or a wrapper (it technically only takes Audio Event metadata
        // that's defined, and just makes sure it's looped, nothing more really)
        source.volume = Mathf.Clamp01(AudioEvent.volume + Random.Range(AudioEvent.volumeShift.x, AudioEvent.volumeShift.y));
        source.pitch = 1.0f + Random.Range(AudioEvent.pitchShift.x, AudioEvent.pitchShift.y);

        source.Play();
    }

    // Kill Sound
    private void OnDestroy()
    {
        source.Stop();
    }
}