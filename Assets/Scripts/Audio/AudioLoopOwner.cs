using UnityEngine;

public class AudioLoopOwner : MonoBehaviour
{
    private void Start()
    {

    }

    private void Update()
    {

    }

    // Helper class that notifies the AudioManager when this GameObject is destroyed,
    // specifically used for looped AudioEvents owned by this object
    // If the Audio is indeed looping, then stop and release the event
    private void OnDestroy()
    {
        AudioManager.Instance.StopOwnedAudio(gameObject);
    }
}