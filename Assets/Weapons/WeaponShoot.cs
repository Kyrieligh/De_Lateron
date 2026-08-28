using UnityEngine;

public class AimWeapon : MonoBehaviour
{
    public GameObject Bullet; //object bullet yang akan di spwan
    public Transform firePoint; //yang mana akan menjadi titik spawn bullet
    public float bulletSpeed = 3f;
    public Vector3 mousePos; // variable mousePos digunakan untuk menyimpan posisi mouse di world position sehingga karakter dapat menghadap ke arah mouse.
    public Camera cam; // variable cam digunakan untuk mengambil posisi mouse di layar dan mengubahnya menjadi world position sehingga karakter dapat menghadap ke arah mouse.
    
    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        mousePos.z = 0;

        Vector3 lookDir = mousePos - transform.position; // rotasi character berdasarkan arah moouse
        float rotz = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotz - 90f); //Quaternion adalah representasi yang digunakan untuk rotasi dalam 3D space

        if (Input.GetKeyDown(KeyCode.Mouse0))  // revisi dari GetKeyDown  menjadi GetKey agar nantinya bisa menembak saat press left mouse button menggunakan GetKey
        {
            Shoot();
        }
    }




    void Shoot() //spawn bullet n shoot
    { 
        //the function instantiate is for spawn object (bullet) in unity
        GameObject bullet = Instantiate(Bullet, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(bullet, 3f);
    }
}
