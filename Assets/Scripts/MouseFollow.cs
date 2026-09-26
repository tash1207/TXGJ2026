using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollow : MonoBehaviour
{
    [SerializeField] int moveSpeed;
    [SerializeField] int dropSpeed;
    [SerializeField] int liftSpeed;
    [SerializeField] float pauseTime;
    [SerializeField] GameObject clawBottom;
    
    InputControls inputActions;
    Rigidbody2D rb;
    bool isDropping = false;

    void Awake()
    {
        inputActions = new InputControls();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Click.performed += OnClick;
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void FixedUpdate()
    {
        if (!isDropping)
        {
            Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
            Vector2 targetPosition = Camera.main.ScreenToWorldPoint(new Vector2(
                mouseScreenPosition.x, 
                mouseScreenPosition.y));
            targetPosition.y = transform.position.y;

            Vector2 direction = targetPosition - rb.position;
            rb.linearVelocity = direction * moveSpeed;
        }
    }

    void OnClick(InputAction.CallbackContext context)
    {
        if (!isDropping)
        {
            StartCoroutine(MoveDownThenUp());
        }
    }

    IEnumerator MoveDownThenUp()
    {
        rb.linearVelocity = new Vector2(0f, 0f);
        isDropping = true;
        Vector2 startPosition = clawBottom.transform.position;
        Vector2 targetPosition = new Vector2(clawBottom.transform.position.x, -1.2f);
        // 1. Drop claw
        while (Vector3.Distance(clawBottom.transform.position, targetPosition) > 0.01f)
        {
            clawBottom.transform.position = Vector3.MoveTowards(clawBottom.transform.position, targetPosition, dropSpeed * Time.deltaTime);
            yield return null;
        }
        clawBottom.transform.position = targetPosition;

        // 2. Pause at the bottom
        // TODO: Play animation of claw opening
        yield return new WaitForSeconds(pauseTime);

        // 3. Move claw back up
        while (Vector3.Distance(clawBottom.transform.position, startPosition) > 0.01f)
        {
            clawBottom.transform.position = Vector3.MoveTowards(clawBottom.transform.position, startPosition, liftSpeed * Time.deltaTime);
            yield return null;
        }
        clawBottom.transform.position = startPosition;
        isDropping = false;
    }
}
