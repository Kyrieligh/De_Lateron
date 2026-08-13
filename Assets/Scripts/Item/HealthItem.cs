using System;
using UnityEngine;

public class HealthItem : MonoBehaviour, ICollectiable
{

    public int healthPoint = 20;

    public static event Action<int> OnHealthCollect;

    public void Collect() //interface implementation
    {
        OnHealthCollect.Invoke(healthPoint);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect();
        }
    }

    //void increaseHealth()
    //{
    //    healthPoint += currentHealth;

    //}
}
