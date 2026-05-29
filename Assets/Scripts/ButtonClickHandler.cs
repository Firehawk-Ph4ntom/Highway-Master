using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonClickHandler : MonoBehaviour
{
    public string sceneName;
    public float transitionDelay = 0.25f;
    public AudioEventData clickSound;
    private bool clicked = false;

    // Give a small transition time for the Button click before moving onto the next scene,
    // giving some time for animation and sounds to play
    public void ClickMenuButton()
    {
        clicked = true;
        AudioManager.Instance.Play(clickSound);
        StartCoroutine(LoadScene());
    }

    public void ClickQuit()
    {
        clicked = true;
        AudioManager.Instance.Play(clickSound);
        StartCoroutine(QuitGame());
    }

    // Load the actual Scene
    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(transitionDelay);
        SceneManager.LoadScene(sceneName);
    }

    // Quit the Application
    private IEnumerator QuitGame()
    {
        yield return new WaitForSeconds(transitionDelay);
        Application.Quit();
    }
}