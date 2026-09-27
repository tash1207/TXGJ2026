using UnityEngine;

public class ClawController : MonoBehaviour
{
    Animator animator;
    SpriteRenderer staticSpriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        staticSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ClawOpen()
    {
        animator.ResetTrigger("Close");
        animator.SetTrigger("Open");

        if(staticSpriteRenderer != null)
        {
            staticSpriteRenderer.enabled = false;
        }
    }

    public void ClawClose()
    {
        animator.ResetTrigger("Open");
        animator.SetTrigger("Close");

        if(staticSpriteRenderer != null)
        {
            staticSpriteRenderer.enabled = true;
        }
    }
}
