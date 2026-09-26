using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollow : MonoBehaviour
{
    [SerializeField] int speed;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector2 targetPosition = Camera.main.ScreenToWorldPoint(new Vector2(
            mouseScreenPosition.x, 
            mouseScreenPosition.y));
        targetPosition.y = transform.position.y;

        Vector2 direction = targetPosition - rb.position;
        rb.linearVelocity = direction * speed;
    }
}
