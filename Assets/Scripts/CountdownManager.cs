using UnityEngine;
using TMPro;
using System.Collections;

public class CountdownManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public AudioEventData countdownSound, countdownSoundCritical, goSound;

    private void Start()
    {
        StartCoroutine(Countdown());
    }

    private void Update()
    {

    }

    // Create a countdown timer before starting the game
    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(0.5f);
        AudioManager.Instance.Play(countdownSound);
        countdownText.text = "3";

        yield return new WaitForSeconds(1.0f);
        AudioManager.Instance.Play(countdownSound);
        countdownText.text = "2";

        yield return new WaitForSeconds(1.0f);
        AudioManager.Instance.Play(countdownSoundCritical);
        countdownText.text = "1";

        yield return new WaitForSeconds(1.0f);
        AudioManager.Instance.Play(goSound);
        countdownText.text = "GO!";

        yield return new WaitForSeconds(1.0f);

        // Disable the text
        countdownText.gameObject.SetActive(false);
        // Start!
        FindFirstObjectByType<GameManager>().gameStarted = true;
    }
}