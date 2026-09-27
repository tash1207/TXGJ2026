using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    int score;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        score = 50;
        CoinManager.Instance.SetCoins(score);
    }

    public int GetScore()
    {
        return score;
    }

    public void AddPoints(int value)
    {
        score += value;
        Debug.Log("Current score: " + score);
        CoinManager.Instance.SetCoins(score);
    }

    public void SubtractPoints(int value)
    {
        score -= value;
        Debug.Log("Current score: " + score);
        CoinManager.Instance.SetCoins(score);
    }
}
