using System.Collections;
using UnityEngine;

public class AimWeapon : MonoBehaviour
{
    public GameObject Bullet; //object bullet yang akan di spwan
    public Transform firePoint; //yang mana akan menjadi titik spawn bullet
    public float bulletSpeed = 3f;
    public Vector3 mousePos; // variable mousePos digunakan untuk menyimpan posisi mouse di world position sehingga karakter dapat menghadap ke arah mouse.
    public Camera cam; // variable cam digunakan untuk mengambil posisi mouse di layar dan mengubahnya menjadi world position sehingga karakter dapat menghadap ke arah mouse.

    [Header("Ammo")]
    public int maxAmmo = 16;
    public int currentAmmo;
    public bool currentlyReloading = false;
    public static AimWeapon instance;

    private void Start()
    {
        currentAmmo = maxAmmo;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        RefreshAmmoUI();
    }
    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        mousePos.z = 0;

        if (currentAmmo < 1) StartCoroutine(Reload());
        if (Input.GetKeyDown(KeyCode.R) && !currentlyReloading && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
        }

        Vector3 lookDir = mousePos - transform.position; // rotasi StatCharacter berdasarkan arah moouse
        float rotz = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotz - 90f); //Quaternion adalah representasi yang digunakan untuk rotasi dalam 3D space

        if (Input.GetKeyDown(KeyCode.Mouse0) && !currentlyReloading)  // revisi dari GetKeyDown  menjadi GetKey agar nantinya bisa menembak saat press left mouse button menggunakan GetKey
        {
            Shoot();
        }
    }


    void Shoot() //spawn bullet n shoot
    {
        currentAmmo--;
        RefreshAmmoUI();

        //the function instantiate is for spawn object (bullet) in unity
        GameObject bullet = Instantiate(Bullet, firePoint.position, firePoint.rotation);
        PlayerBullet pBullet = bullet.GetComponent<PlayerBullet>();
        if (pBullet != null)
        {
            // Set damage peluru agar sama dengan AttackDamage milik Player saat ini
            pBullet.damage = Player.Instance.AttackDamage;
        }
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(bullet, 3f);
    }

    IEnumerator Reload()
    {
        currentlyReloading = true;
        yield return new WaitForSeconds(1.7f);
        currentAmmo = maxAmmo;
        currentlyReloading = false;

        RefreshAmmoUI();
    }

    private void RefreshAmmoUI()
    {
        if (Ammo.instance != null)
        {
            Ammo.instance.UpdateAmmoUI(currentAmmo, maxAmmo);
        }
    }
}
