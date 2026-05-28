using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AmbientStreamPlayer : MonoBehaviour
{
    public AudioClip ambientClip;

    [Range(0f, 1f)]
    public float streamVolume;
    public bool randomStart = true;
    public float fadeInDuration = 3.0f;
    public float fadeOutDuration = 1.5f;

    public AudioMixer audioMixer;
    public string volumeParameter;

    private AudioSource source;

    private void Start()
    {
        // Call AudioSource component, set clip, loop, and volume, then play
        source = GetComponent<AudioSource>();
        source.clip = ambientClip;
        source.loop = true;
        source.volume = streamVolume;

        // Start muted in mixer
        SetMixerVolume(0.0001f);

        source.Play();

        // Random playback offset
        if (randomStart)
        {
            source.time = Random.Range(0f, ambientClip.length);
        }

        // Start the audio clip faded in for a smooth transition from silence to the ambient sound
        StartCoroutine(FadeInRoutine());
    }

    private void Update()
    {

    }

    // Fade ambience in
    private IEnumerator FadeInRoutine()
    {   
        // Start with the volume at 0, then gradually increase it to the target volume over the fadeInDuration
        float timer = 0.0f;
        while (timer < fadeInDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeInDuration;

            // Use Mathf.Lerp to interpolate the volume/slowly increase the volume over t
            float volume = Mathf.Lerp(0.0001f, 1.0f, t);
            SetMixerVolume(volume);

            yield return null;
        }
        SetMixerVolume(1.0f);
    }

    // Subroutine to fade out ambience, called from GameManager when Game Over is triggered
    public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine());
    }

    // Fade ambience out
    // Inverse FadeIn
    private IEnumerator FadeOutRoutine()
    {
        float timer = 0.0f;
        while (timer < fadeOutDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeOutDuration;

            // Use Mathf.Lerp to interpolate the volume/slowly decrease the volume over t
            float volume = Mathf.Lerp(1.0f, 0.0001f, t);
            SetMixerVolume(volume);

            yield return null;
        }
        SetMixerVolume(0.0001f);
        // Stop playing
        source.Stop();
    }

    // Convert volume to decibels, then apply it to the Audio Mixer
    private void SetMixerVolume(float linearVolume)
    {
        // This took a while to figure out, but volume in Audio Mixer is set in decibels, 
        // so we need to convert linear 0-1 volume to decibels using the formula: 20 * log10(volume)
        float mixerVolume = Mathf.Log10(linearVolume) * 20.0f;
        audioMixer.SetFloat(volumeParameter, mixerVolume);
    }
}