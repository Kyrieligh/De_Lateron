using UnityEngine;

public class Player : Character
{
    //public int currentHealth;

    void takeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
