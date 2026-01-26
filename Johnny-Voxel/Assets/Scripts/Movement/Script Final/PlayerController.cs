using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;
    public LayerMask groundMask;
    

    public float speed = 3f;
    public float animationSmooth = 0.1f;
    float gravity = -9.81f;
    float verticalVelocity;

    private Camera mainCamera;
    private Vector2 movementInput;
    private Vector3 lookPos;
    

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
        {

        }
    }

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        ReadMovement();
        Move();
        RotateTowardsMouse();
        UpdateAnimator();

        if (characterController.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 move = new Vector3(movementInput.x, verticalVelocity, movementInput.y);
        characterController.Move(move * Time.deltaTime);
    }

    void ReadMovement()
    {
        if (Keyboard.current == null) return;

        movementInput.x =
            (Keyboard.current.dKey.isPressed ? 1 : 0) -
            (Keyboard.current.aKey.isPressed ? 1 : 0);

        movementInput.y =
            (Keyboard.current.wKey.isPressed ? 1 : 0) -
            (Keyboard.current.sKey.isPressed ? 1 : 0);

        if (movementInput.magnitude > 1)
            movementInput.Normalize();
    }

    void Move()
    {
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        Vector3 trueMove = transform.rotation * move;
        characterController.Move(trueMove * speed * Time.deltaTime);
    }

    void RotateTowardsMouse()
    {
        if (Mouse.current == null) return;

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, groundMask))
        {
            lookPos = hit.point;
            Vector3 lookDir = lookPos - transform.position;
            lookDir.y = 0;

            if (lookDir.sqrMagnitude > 0.001f)
                transform.forward = lookDir;
        }
    }

    void UpdateAnimator()
    {
        Vector3 localMove =
            transform.InverseTransformDirection(
                new Vector3(movementInput.x, 0, movementInput.y)
            );

        animator.SetFloat(
            "Horizontal",
            Mathf.Lerp(animator.GetFloat("Horizontal"), localMove.x, animationSmooth)
        );

        animator.SetFloat(
            "Vertical",
            Mathf.Lerp(animator.GetFloat("Vertical"), localMove.z, animationSmooth)
        );
    }

}

