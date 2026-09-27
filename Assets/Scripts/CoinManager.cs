using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    List<Coin> coins;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        coins = new List<Coin>();

        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Coin>(out var coin))
            {
                coins.Add(coin);
            }
        }
        SetCoins(ScoreManager.Instance.GetScore());
    }

    public void SetCoins(int scoreValue)
    {
        int coinValue = scoreValue / 10;
        
        for (int i = 0; i < coins.Count; i++)
        {
            if (i < coinValue)
            {
                coins[i].SetHasCoin();
            }
            else
            {
                coins[i].SetEmpty();
            }
        }
    }
}
