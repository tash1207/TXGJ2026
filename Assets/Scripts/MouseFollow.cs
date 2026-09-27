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
    [SerializeField] GameObject heldPackage;
    [SerializeField] Rigidbody2D rbPackage;
    
    InputControls inputActions;
    ClawController clawController;
    Rigidbody2D rbClaw;
    bool isDropping = false;
    bool isOpen = false;
    bool isAttached = false;

    void Awake()
    {
        inputActions = new InputControls();
        clawController = FindAnyObjectByType<ClawController>();
    }

    void Start()
    {
        rbClaw = GetComponent<Rigidbody2D>();
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Package") && !isAttached)
        {
            Debug.Log(collision.gameObject);
            //Package package = collision.gameObject.GetComponent<Package>();
            //heldPackage.GetComponentInChildren<SpriteRenderer>().sprite = package.packageSprite;
            //heldPackage.GetComponent<HeldPackage>().pointValue = package.pointValue;
            //heldPackage.SetActive(true);
            //isAttached = true;
            //Destroy(collision.gameObject);

            heldPackage = collision.gameObject;

            heldPackage.transform.SetParent(clawBottom.transform, worldPositionStays: false);
            heldPackage.transform.localPosition = new Vector3(0f, -0.8f, 0f);
            heldPackage.transform.localRotation = Quaternion.identity;

            rbPackage = heldPackage.GetComponent<Rigidbody2D>();

            if (rbPackage != null)
            {
                rbPackage.bodyType = RigidbodyType2D.Kinematic;
                rbPackage.linearVelocity = Vector2.zero;
                rbPackage.angularVelocity = 0f;
            }

            isAttached = true;
        }
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

            Vector2 direction = targetPosition - rbClaw.position;
            rbClaw.linearVelocity = direction * moveSpeed;
            
            if(rbPackage != null)
            {
                rbPackage.linearVelocity = direction * moveSpeed;
            }
        }
    }

    void OnClick(InputAction.CallbackContext context)
    {
        if (isDropping) return;
        if (!isOpen)
        {
            StartCoroutine(MoveDownThenUp());
        }
        else
        {
            clawController.ClawClose();
            isOpen = false;
            if (isAttached)
            {
                // Drop Package
                //int points = heldPackage.GetComponent<HeldPackage>().pointValue;
                //ScoreManager.Instance.AddPoints(points);
                //heldPackage.SetActive(false);
                //isAttached = false;
                DetachAndDropPackage();
            }
        }
    }

    IEnumerator MoveDownThenUp()
    {
        //rbClaw.linearVelocity = new Vector2(0f, 0f);
        //rbPackage.linearVelocity = new Vector2(0f, 0f);
        rbClaw.linearVelocity = Vector2.zero;
        if(rbPackage != null)
        {
            rbPackage.linearVelocity = Vector2.zero;
        }

        isDropping = true;
        clawController.ClawOpen();
        isOpen = true;
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

    void DetachAndDropPackage()
    {
        if(heldPackage != null)
        {
            heldPackage.transform.SetParent(null);

            HeldPackage packageComponent = heldPackage.GetComponent<HeldPackage>();

            if(packageComponent != null)
            {
                ScoreManager.Instance.AddPoints(packageComponent.pointValue);
            }

            Collider2D col = heldPackage.GetComponent<Collider2D>();
            
            if (col != null)
            {
                StartCoroutine(EnableColliderRoutine(col));
            }

            if (rbPackage != null)
            {
                rbPackage.bodyType = RigidbodyType2D.Dynamic;
                rbPackage.gravityScale = 2f;
                rbPackage.linearVelocity = Vector2.zero;
            }
        }

        rbPackage = null;
        isAttached = false;
        heldPackage = null;
    }

    IEnumerator EnableColliderRoutine(Collider2D col)
    {
        col.enabled = false;
        yield return new WaitForSeconds(0.2f);
        if (col != null) col.enabled = true;
    }
}