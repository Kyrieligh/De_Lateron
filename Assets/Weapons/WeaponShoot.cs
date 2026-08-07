using UnityEngine;

public class AimWeapon : MonoBehaviour
{
    public GameObject Bullet; //object bullet yang akan di spwan
    public Transform firePoint; //yang mana akan menjadi titik spawn bullet
    public float bulletSpeed = 20f;
    public Vector3 mousePos; // variable mousePos digunakan untuk menyimpan posisi mouse di world position sehingga karakter dapat menghadap ke arah mouse.
    public Camera cam; // variable cam digunakan untuk mengambil posisi mouse di layar dan mengubahnya menjadi world position sehingga karakter dapat menghadap ke arah mouse.
    public int damage = 5;
    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;
        float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotz - 90f);

        if (Input.GetKeyDown(KeyCode.Mouse0))  // revisi dari GetKeyDown  menjadi GetKey agar nantinya bisa menembak saat press left mouse button menggunakan GetKey
        {
            Shoot();
        }
    }




    void Shoot() //spawn bullet
    { 
        //the function instantiate is for spawn object (bullet) in unity
        GameObject bullet = Instantiate(Bullet, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(bullet, 3f);
    }
}
