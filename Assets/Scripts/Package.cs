using UnityEngine;

public class Package : MonoBehaviour
{
    public Sprite packageSprite;
    public Sprite workPackageSprite;
    public int pointValue;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        spriteRenderer.sprite = GameManager.Instance.isWorkMode ? workPackageSprite : packageSprite;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bottom") || collision.gameObject.CompareTag("Package"))
        {
            // Change from HeldPackage back to Package
            gameObject.tag = "Package";
        }
    }

    public void EnableWorkMode()
    {
        spriteRenderer.sprite = workPackageSprite;
    }
}
