using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollow : MonoBehaviour
{
    public int speed;

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        // Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // targetPos.y = transform.position.y;
        // targetPos.z = 0f;

        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(
            mouseScreenPosition.x, 
            mouseScreenPosition.y, 
            0f));
        targetPosition.y = transform.position.y;
        targetPosition.z = 0f;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
