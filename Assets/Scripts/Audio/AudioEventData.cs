using UnityEngine;
using UnityEngine.Audio;

// Instantiate Asset Menu for Audio Events
[CreateAssetMenu(menuName = "Audio/Audio Event")]
public class AudioEventData : ScriptableObject
{
    public AudioClip[] clips;

    [Range(0f, 1f)]
    public float volume = 1.0f;

    public Vector2 volumeShift = Vector2.zero;
    public Vector2 pitchShift = Vector2.zero;
    public Vector2 delay = Vector2.zero;

    public int limit = 0;
    public AudioMixerGroup mixerGroup;
}