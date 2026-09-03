using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 5;

   public int Damage
    {
        get => damage;
        set => damage = value;
    }

}