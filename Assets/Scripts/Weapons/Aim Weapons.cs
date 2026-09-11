using UnityEngine;

public class ParentWeapon : MonoBehaviour
{
    public Vector2 PointerPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if(direction.x < 0)
        {
            scale.y = -1;
        }else if (direction.x < 0)
        {
            scale.y = 1;
        }
        transform.localScale = scale;
    }


}
