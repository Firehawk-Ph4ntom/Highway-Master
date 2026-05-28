using UnityEngine;

public class AudioLoopClientBehavior : MonoBehaviour
{
    public AudioEventData AudioEvent;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();

        AudioClip clip = AudioEvent.clips[Random.Range(0, AudioEvent.clips.Length)];

        source.clip = clip;
        source.loop = true;
        source.outputAudioMixerGroup = AudioEvent.mixerGroup;

        source.volume = Mathf.Clamp01(AudioEvent.volume + Random.Range(AudioEvent.volumeShift.x, AudioEvent.volumeShift.y));
        source.pitch = 1.0f + Random.Range(AudioEvent.pitchShift.x, AudioEvent.pitchShift.y);

        source.Play();
    }

    private void OnDestroy()
    {
        source.Stop();
    }
}