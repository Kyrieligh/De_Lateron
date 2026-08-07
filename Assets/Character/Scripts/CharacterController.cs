using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb2D; // revisi nama variable sebelumnya rigidbody2D menjadi rb2D karena rigidbody2D API lama
    private Vector3 moveDir;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    private void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }

        moveDir = new Vector3(moveX, moveY).normalized; //normalize vector to make sure not go to faster
    }

    private void FixedUpdate()
    {
        rb2D.linearVelocity = moveDir; //Perubahan dari velocity menjadi linearVelocity ini terjadi karena menggunakan Unity versi baru
    }

}
