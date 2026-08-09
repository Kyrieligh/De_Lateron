using UnityEngine;

public class GameMechanic : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform target;

    [Header("State / Input")]
    [SerializeField] private Vector2 moveDirection;
    private Vector2 pointerInput;
    private Vector2 moveInput;
    private Animator animator;

    // GETTER & SETTER (PROPERTIES

    // 1. Move Speed (Validasi agar Kecepatan Tidak Minus)
    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = Mathf.Max(0f, value); // Memastikan kecepatan tidak negatif
    }

    // 2. Target (Bisa dibaca dan diubah dari script AI / System lain)
    public Transform Target
    {
        get => target;
        set => target = value;
    }

    // 3. Move Direction (Arah Gerak Karakter)
    public Vector2 MoveDirection
    {
        get => moveDirection;
        set => moveDirection = value.normalized; // Menggunakan .normalized agar panjang vector bernilai 1
    }

    // 4. Move Input & Pointer Input (Biasanya di-set oleh Input Reader/PlayerController)
    public Vector2 MoveInput
    {
        get => moveInput;
        set => moveInput = value;
    }

    public Vector2 PointerInput
    {
        get => pointerInput;
        set => pointerInput = value;
    }

    // 5. Read-Only Components (Hanya Getter agar script luar tidak merusak referensi)
    public Rigidbody Rb => rb;
    public Animator Animator => animator;

    // ========================================================
    // UNITY LIFECYCLE (Menggantikan Constructor)
    // ========================================================

    private void Awake()
    {
        // Ambil komponen secara otomatis jika belum di-assign di Inspector
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (animator == null) animator = GetComponent<Animator>();
    }
}