using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public int Poin;
    public TMP_Text pointText;
    public static ScoreManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int CurrentPoint => Poin;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void UpdatePoint(int points)
    {
        Poin += points;
        pointText.text = "Poin : " + Poin;
    }
}
