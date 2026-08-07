using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class bullet : MonoBehaviour
{
    public int damage = 5;
    private ScoreManager scoreManager; // call the ScoreManager script
    bool hitTaget;

    void Start()
    {
        scoreManager = GameObject.FindWithTag("Score").GetComponent<ScoreManager>(); //find the ScoreManager script    
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null && collision.CompareTag("Enemy"))
        {
            enemy.TakeDamage(damage);
            scoreManager.UpdateScore(scoreManager.score);
            Destroy(gameObject);
        }        
    }

}
