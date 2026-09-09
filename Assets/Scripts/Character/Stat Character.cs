using UnityEngine;

public class StatCharacter : MonoBehaviour
{
    
    [Header("Base Attributes")]
    [SerializeField] protected string characterName;
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int currentHealth;
    [SerializeField] protected int attackDamage;
    
    [Header("Movement Settings")]
    [SerializeField] protected float moveSpeed = 5f;

    [Header("References")]
    protected Rigidbody2D rb;

    [Header("State / Input")]
    [SerializeField] protected Vector2 moveDirection;
    protected Vector2 pointerInput;
    protected Vector2 moveInput;
    protected Animator animator;

    // Default Constructor
    //public StatCharacter()
    //{
    //    characterName = "Unnamed";
    //    maxHealth = 100;
    //    currentHealth = 100;
    //    attackDamage = 15;
    //}

    // Parameterized Constructor
    //public StatCharacter(string characterName, int maxHealth, int attackDamage)
    //{
    //    this.characterName = characterName;
    //    this.maxHealth = maxHealth;
    //    this.currentHealth = maxHealth;
    //    this.attackDamage = attackDamage;
    //}

    // Public Getters and Setters

    public string CharacterName
    {
        get => characterName;
        set => characterName = value;
    }

    public int MaxHealth
    {
        //Kode ini memastikan bahwa nilai maxHealth minimal bernilai 1.
        //Jika ada kode/sistem lain yang mencoba mengisi maxHealth dengan angka 0 atau negatif (misal -50),
        //Mathf.Max akan membandingkan 1 dan -50, lalu mengambil angka yang paling besar, yaitu 1.
        get => maxHealth;
        set => maxHealth = Mathf.Max(1, value); //
    }

    public int CurrentHealth
    {
        //Kode ini membatasi (clamping) nilai currentHealth agar selalu berada di dalam rentang rentang tertentu, yaitu antara 0 sampai maxHealth.
        //Mathf.Clamp(nilai, min, max) menjaga nilai agar tidak kurang dari min dan tidak lebih dari max
        get => currentHealth;
        set => currentHealth = Mathf.Clamp(value, 0, maxHealth);
    }

    public int AttackDamage
    {
        get => attackDamage;
        set => attackDamage = Mathf.Max(0, value);
    }

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    //protected virtual void Awake()
    //{
    //    currentHealth = maxHealth;
    //}


}
