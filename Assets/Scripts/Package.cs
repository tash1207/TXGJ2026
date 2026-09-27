using UnityEngine;

public class Package : MonoBehaviour
{
    public Sprite packageSprite;
    public int pointValue;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bottom") || collision.gameObject.CompareTag("Package"))
        {
            // Change from HeldPackage back to Package
            gameObject.tag = "Package";
        }
    }
}
