using UnityEngine;

// A Client Behavior off AudioEventData, for any Audio Playback, but here I'll mainly use it for Prefab Audio Loops
public class AudioClientBehavior : MonoBehaviour
{
    public AudioEventData audioEvent;

    private void Start()
    {
        // Play an AudioEvent on GameObject directly
        AudioManager.Instance.Play(audioEvent, gameObject);
    }

    private void Update()
    {

    }

    // Notify the AudioManager when this GameObject is destroyed, specifically used for looped AudioEvents owned by this object
    // If the Audio is indeed looping, then stop and release the event
    // This is done because a Looping AudioEvent has issues with decrementing limit count and stopping playback when necessary (upon GameObject destruction)
    private void OnDestroy()
    {
        AudioManager.Instance.StopOwnedAudio(gameObject);
    }
}