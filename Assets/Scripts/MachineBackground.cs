using UnityEngine;

public class MachineBackground : MonoBehaviour
{
    public Sprite workBackgroundSprite;
    //SpriteRenderer spriteRenderer;
    Animator animator;

    void Awake()
    {
        //spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void EnableWorkMode()
    {
        animator.SetTrigger("Goggles");
        //spriteRenderer.sprite = workBackgroundSprite;
    }
}
