using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] public float moveSpeed = 5f;
    private Vector2 pointerInput, moveInput; // variable moveInput digunakan untuk menyimpan input dari player yang berupa Vector2 sehingga karakter dapat bergerak ke arah input yang diberikan.
    private Animator animator;
    //[SerializeField] InputActionReference pointerPosition;
    //private WeaponParent weaponParent;
    //private Vector3 mousePos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    

    //private Vector2 GetPointerInput()
    //{
    //    Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
    //    mousePos.z = Camera.main.nearClipPlane;
    //    return Camera.main.ScreenToWorldPoint(mousePos);
    //}

    //private void Awake()
    //{
    //    weaponParent = GetComponent<WeaponParent>();
    //}

    public void Move(InputAction.CallbackContext context) //Move akan mucul pada event di Component player input. //membuat variable parameter juga bisa tidak perlu membuat diatas seperto public float
    {
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

    //public void Jump(InputAction.CallbackContext context)//jadi parameter "context" bisa digunakan atau di update lagi karena menyimpan methode move yang mana nanti akan di update penambahan fitur jump
    //{
    //    if (context.performed)//if player press jump
    //    {
    //        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    //    }else if (context.canceled)//if i hold jump button player will jump higher
    //    {
    //        rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
    //    }
    //}
}
