using UnityEngine;
using UnityEngine.InputSystem;

public class GameMechanic : MonoBehaviour, IMovable
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("References")]
    [SerializeField] protected Transform target;
    private Rigidbody2D rb;

    [Header("State / Input")]
    [SerializeField] protected Vector2 moveDirection;
    protected Vector2 pointerInput;
    protected Vector2 moveInput;
    protected Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Di C# Unity, fungsi GetComponent<T>() sebenarnya adalah singkatan dari this.gameObject.GetComponent<T>().
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        //weaponParent.PointerPosition = pointerInput;
        //pointerInput = GetPointerInput();


    }

    public void Move(Vector2 direction)
    {
        moveInput = direction;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Move(context.ReadValue<Vector2>());
        animator.SetBool("isWalking", true);// dari UnityEngine memakai library atau tools "Animator" terus mengambil variable animator.if player press move button then isWalking will be true and play walk 
        if (context.canceled) //if player release move button then isWalking will be false and play idle animation
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }

        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }
}