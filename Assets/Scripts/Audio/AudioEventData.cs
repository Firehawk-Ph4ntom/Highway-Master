using UnityEngine;
using UnityEngine.Audio;

// Instantiate Asset Menu for Audio Events
[CreateAssetMenu(menuName = "Audio/Audio Event")]
public class AudioEventData : ScriptableObject
{
    public AudioClip[] clips;

    [Range(0f, 1f)]
    public float volume = 1.0f;

    public Vector2 volumeShift = new Vector2(0, 0);
    public Vector2 pitchShift = new Vector2(0, 0);
    public Vector2 delay = new Vector2(0, 0);

    public bool loop = false;

    public int limit = 0;
    public AudioMixerGroup mixerGroup;
}