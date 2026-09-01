using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor.ShaderGraph.Internal;

public class Player : Character, IDamageable
{
    public GameOver kockMati;
    // im using "new" for i can modify this variable in StatUpgrade.cs otherwise its onlly update PLayer 
    //public new float moveSpeed = 6.9f; 
    //public new int maxHealth = 100;
    //public new int attackDamage = 15;

    [Header("Health Bar")]
    public Slider slider;
    [Header("Player Specific Attributes")]
    [SerializeField] private int weaponDamage = 50;

    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private TrailRenderer trailRenderer;

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

        //kockMati = Object.FindAnyObjectByType<GameOver>(); // Find the GameOver script in the scene
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing) return;
        rb.linearVelocity = moveInput * moveSpeed;
        //weaponParent.PointerPosition = pointerInput;
        //pointerInput = GetPointerInput();
        



    }

    public void onDashInput(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
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

    private IEnumerator Dash()
    {
        canDash = false;        
        isDashing = true;

        //enable ignore collision while dash pass through enemy
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);

        // Ambil arah dari tombol WASD yang sedang ditekan
        Vector2 dashDirection = moveInput.normalized;

        // Jika Player sedang diam (tidak tekan WASD), gunakan arah hadap X
        if (dashDirection == Vector2.zero)
        {
            // Cek apakah karakter sedang hadap kiri (scale negatif atau flipX)
            float facingDirection = transform.localScale.x >= 0 ? 1f : -1f;
            dashDirection = new Vector2(facingDirection, 0f);
        }

        // memberikan kecepatan dash ke arah yang ditentukan
        rb.linearVelocity = dashDirection * dashingPower;

        if (trailRenderer != null)
        {
            trailRenderer.emitting = true;
        }

        yield return new WaitForSeconds(dashingTime);

        //disable ignore collision while dash pass through enemy
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);

        if (trailRenderer != null)
        {
            trailRenderer.emitting = false;
        }

        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
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
        Debug.Log("ittai yooooo");
        CurrentHealth -= damageAmount;
        slider.value = CurrentHealth;
        if (CurrentHealth <= 0)
        {
            die();
        }
    }

    void die()
    {
            kockMati.TriggerGameOver();
        
        Destroy(gameObject);
        
    }

    void Heal(int amount)
    {
        // Menggunakan property CurrentHealth otomatis dibatasi batas atas di class Character
        CurrentHealth += amount;
        
        if (slider != null) slider.value = CurrentHealth;
    }


}
