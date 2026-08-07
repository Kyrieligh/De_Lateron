using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public int score;
    public TMP_Text scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score : " + score;
    }
}
