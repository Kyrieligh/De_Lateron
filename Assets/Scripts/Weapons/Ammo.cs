using UnityEngine;
using TMPro;

public class Ammo : MonoBehaviour
{
    public TMP_Text ammoText;
    public static Ammo instance; //digunakan untuk membuat instance dari class Ammo agar dapat diakses dari class lain

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void UpdateAmmoUI(int currentAmmoUI, int maxAmmoUI)
    {
        ammoText.text = $"{currentAmmoUI} / {maxAmmoUI}" ;
    }
}
