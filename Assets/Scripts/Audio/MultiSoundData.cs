using UnityEngine;

[CreateAssetMenu(menuName = "Audio/MultiSound")]
public class MultiSoundData : ScriptableObject
{
    public AudioEventData[] audioEvents;
}