using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] float timeBeforeGameOverScreen = 2f;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject winImage;
    [SerializeField] GameObject loseImage;

    public bool isWorkMode;
    public static bool isEndlessMode = false;

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

    public void EndGameLose()
    {
        EndGame(false);
    }

    public void EndGameWin()
    {
        if (isEndlessMode) return;
        EndGame(true);
    }

    void EndGame(bool wonGame)
    {
        isWorkMode = true;
        MusicHandler.Instance.PlayEndingMusic();
        Package[] allPackages = FindObjectsByType<Package>(FindObjectsSortMode.None);
        foreach (var package in allPackages)
        {
            package.EnableWorkMode();
        }
        FindAnyObjectByType<MachineBackground>().EnableWorkMode();
        ShowGameOverScreenAfterDelay(wonGame);
    }

    public void ShowGameOverScreenAfterDelay(bool wonGame)
    {
        StartCoroutine(ShowGameOver(wonGame));
    }

    private IEnumerator ShowGameOver(bool wonGame)
    {
        yield return new WaitForSeconds(timeBeforeGameOverScreen);
        if (wonGame)
        {
            winImage.SetActive(true);
        }
        else
        {
            loseImage.SetActive(true);
        }
        gameOverScreen.SetActive(true);
    }
}
