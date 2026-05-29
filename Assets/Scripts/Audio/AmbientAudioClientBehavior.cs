using UnityEngine;
using System.Collections;

// Serializable class to hold Ambient Audio Event Weight Prefab data
[System.Serializable]
public class AmbientAudioEventWeightData
{
    public AudioEventData audioEvent;
    public float playWeight = 1.0f;
}

public class AmbientAudioClientBehavior : MonoBehaviour
{
    public AmbientAudioEventWeightData[] ambientAudioEventWeightData;

    public float minDelay;
    public float maxDelay;

    private void Start()
    {
        StartCoroutine(AmbientRoutine());
    }

    // Continuosly play Ambient Audio, unless GameOver
    private IEnumerator AmbientRoutine()
    {
        // FindFirstObjectByType<GameManager>().gameStarted (weird to not start ambient audio unless game start, we're already in the scene!)
        while (!FindFirstObjectByType<GameManager>().gameOver)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            // From a range of Ambient AudioEvents, pick one (Weight system exact copy from Obstacle)
            AudioEventData selectedEvent = PickWeightedAudioEvent();
            AudioManager.Instance.Play(selectedEvent);
        }
    }

    // Weighted system, allowing Audio Events to be prioritized based on a number assigned
    // which the higher the number, the likelier it is to play compared to the other Audio Events
    private AudioEventData PickWeightedAudioEvent()
    {
        // Get total event weight
        float totalWeight = GetTotalAudioWeight();

        // If no weight set, then don't play anything
        if (totalWeight <= 0.0f)
            return null;

        // Randomized interval between 0 and total weight
        float randomValue = Random.Range(0.0f, totalWeight);

        for (int i = 0; i < ambientAudioEventWeightData.Length; i++)
        {
            AmbientAudioEventWeightData data = ambientAudioEventWeightData[i];

            // If event weight is 0, then it shouldn't play at all/chosen for calculation
            if (data.playWeight <= 0.0f)
                continue;

            // The range calculation
            randomValue -= data.playWeight;

            // The event that passes the negative check first will be the one that gets spawned
            if (randomValue <= 0.0f)
                return data.audioEvent;
        }
        return null;
    }

    // Based on the Inspector, the total weight is calculated by just iterating through the numbers
    // of all the Audio Events and summing them up, which is then used for the weighted randomization
    private float GetTotalAudioWeight()
    {
        float totalWeight = 0.0f;

        for (int i = 0; i < ambientAudioEventWeightData.Length; i++)
        {
            AmbientAudioEventWeightData data = ambientAudioEventWeightData[i];

            if (data.playWeight > 0.0f)
                totalWeight += data.playWeight;
        }
        return totalWeight;
    }
}