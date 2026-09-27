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
            // Don't change if hitting a package while being lifted with claw.
            GameObject clawBottom = GameObject.FindGameObjectWithTag("ClawBottom");
            if (Vector3.Distance(transform.position, clawBottom.transform.position) > 1f)
            {
                // Change from HeldPackage back to Package when dropped
                gameObject.tag = "Package";
            }
        }
    }

    public void EnableWorkMode()
    {
        spriteRenderer.sprite = workPackageSprite;
    }
}
