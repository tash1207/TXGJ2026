using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] float timeBeforeGameOverScreen = 2f;
    [SerializeField] GameObject gameOverScreen;

    public bool isWorkMode;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Replay()
    {
        MusicHandler.Instance.PlayGameMusic();
        SceneManager.LoadScene(1);
    }

    public void StartMenu()
    {
        MusicHandler.Instance.PlayGameMusic();
        SceneManager.LoadScene(0);
    }

    public void ShowGameOverScreenAfterDelay()
    {
        StartCoroutine(ShowGameOver());
    }

    private IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(timeBeforeGameOverScreen);
        gameOverScreen.SetActive(true);
    }
}
