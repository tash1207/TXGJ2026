using UnityEngine;

public class MachineBackground : MonoBehaviour
{
    public Sprite workBackgroundSprite;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void EnableWorkMode()
    {
        spriteRenderer.sprite = workBackgroundSprite;
    }
}
