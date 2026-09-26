using UnityEngine;

public class ClawController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ClawOpen()
    {
        animator.SetTrigger("Open");
    }

    public void ClawClose()
    {
        animator.SetTrigger("Close");
    }
}
