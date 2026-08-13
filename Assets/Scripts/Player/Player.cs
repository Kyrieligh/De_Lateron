using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : Character, IDamageable
{
    [Header("Health Bar")]
    public Slider slider;
    [Header("Player Specific Attributes")]
    [SerializeField] private int weaponDamage = 50;


    public string changeName
    {
        get => characterName;
        set => characterName = value;
    }
    public int WeaponDamage
    {
        get => weaponDamage;
        set => weaponDamage = value;
    }

    void Start()
    {   
        rb = GetComponent<Rigidbody2D>(); //Di C# Unity, fungsi GetComponent<T>() sebenarnya adalah singkatan dari this.gameObject.GetComponent<T>().
        animator = GetComponent<Animator>();
        CurrentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = CurrentHealth;
        HealthItem.OnHealthCollect += Heal;
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        //weaponParent.PointerPosition = pointerInput;
        //pointerInput = GetPointerInput();


    }

    //to avoid Memory leak
    private void OnDestroy()
    {
        HealthItem.OnHealthCollect -= Heal;
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


    //public int currentHealth;
    public void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        slider.value = CurrentHealth;
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Heal(int amount)
    {
        // Menggunakan property CurrentHealth otomatis dibatasi batas atas di class Character
        CurrentHealth += amount;
        
        if (slider != null) slider.value = CurrentHealth;
    }


}
