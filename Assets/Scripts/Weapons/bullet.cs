using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected int damage = 5;

   public int Damage
    {
        get => damage;
        set => damage = value;
    }

}