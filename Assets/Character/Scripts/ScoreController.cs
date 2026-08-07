using UnityEngine;
using UnityEngine.Events;

public class ScoreController : MonoBehaviour
{
    public UnityEvent OnScoreChanged;
    public int Score { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int amount)
    {
        Score += amount;
        OnScoreChanged.Invoke(); //invoke adalah untuk "memanggil" event OnScoreChanged.
    }

}
