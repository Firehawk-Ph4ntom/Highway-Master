using UnityEngine;

// Instantiate Asset Menu for Multi Sounds
[CreateAssetMenu(menuName = "Audio/MultiSound")]
public class MultiSoundData : ScriptableObject
{
    public AudioEventData[] audioEvents;
}
// This is just to instantiate multiple Audio Events from a single source by being called once