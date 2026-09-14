using Unity.VisualScripting;
using UnityEngine;

public class ParentWeapon : MonoBehaviour
{

    [Header("Renderers for Layering")]
    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private SpriteRenderer weaponRenderer;
    
    public Vector2 PointerPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotate the pivot 
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        if (direction.sqrMagnitude > 0.001f)
        {
            transform.right = direction;

        }

        // flip weapon
        Vector3 scale = transform.localScale;
        if (direction.x < 0)
        {
            scale.y = -Mathf.Abs(scale.y); // face left flip to bottom
        }
        else if (direction.x > 0)
        {
            scale.y = Mathf.Abs(scale.y);
        }
        transform.localScale = scale; // face right flip normal


        // render behind player when aiming upward
        if (characterRenderer != null && weaponRenderer != null)
        {
            if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
            {
                weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
            }
            else
            {
                weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
            }
        }
    }

}
