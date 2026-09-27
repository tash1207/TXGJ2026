using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void Play()
    {
        GameManager.isEndlessMode = false;
        SceneManager.LoadScene(1);
    }

    public void Endless()
    {
        GameManager.isEndlessMode = true;
        SceneManager.LoadScene(1);
    }
}
