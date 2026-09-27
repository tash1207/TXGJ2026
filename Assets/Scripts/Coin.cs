using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [SerializeField] Sprite coin;
    [SerializeField] Sprite emptyCoin;

    public bool hasCoin = false;
    Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetHasCoin()
    {
        image.sprite = coin;
        hasCoin = true;
    }

    public void SetEmpty()
    {
        image.sprite = emptyCoin;
        hasCoin = false;
    }
}
