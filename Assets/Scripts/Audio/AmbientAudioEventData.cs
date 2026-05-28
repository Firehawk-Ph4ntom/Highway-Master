using UnityEngine;
using System.Collections;

[System.Serializable]
public class AudioEventWeight
{
    public AudioEventData audioEvent;
    public float weight = 1.0f;
}

public class AmbientAudioEventData : MonoBehaviour
{
    public AudioEventWeight[] ambientEvents;

    public float minDelay = 3.0f;
    public float maxDelay = 8.0f;

    private void Start()
    {
        StartCoroutine(AmbientRoutine());
    }

    private void Update()
    {

    }

    private IEnumerator AmbientRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(
                Random.Range(minDelay, maxDelay)
            );

            AudioEventData selectedEvent = PickWeightedAudioEvent();

            if (selectedEvent != null &&
                AudioManager.Instance != null)
            {
                AudioManager.Instance.Play(selectedEvent);
            }
        }
    }

    private AudioEventData PickWeightedAudioEvent()
    {
        float totalWeight = GetTotalAudioWeight();

        if (totalWeight <= 0.0f)
            return null;

        float randomValue =
            Random.Range(0.0f, totalWeight);

        for (int i = 0; i < ambientEvents.Length; i++)
        {
            WeightedAudioEvent entry = ambientEvents[i];

            if (entry.audioEvent == null ||
                entry.weight <= 0.0f)
            {
                continue;
            }

            randomValue -= entry.weight;

            if (randomValue <= 0.0f)
                return entry.audioEvent;
        }

        return null;
    }

    private float GetTotalAudioWeight()
    {
        float totalWeight = 0.0f;

        for (int i = 0; i < ambientEvents.Length; i++)
        {
            WeightedAudioEvent entry = ambientEvents[i];

            if (entry.audioEvent != null &&
                entry.weight > 0.0f)
            {
                totalWeight += entry.weight;
            }
        }

        return totalWeight;
    }
}