using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatUpgrade : MonoBehaviour
{
    public enum UpgradeType
    {
        MoveSpeed,
        MaxHealth,
        Damage
    }

    [Header("Upgrade Type")]
    [SerializeField] private UpgradeType upgradeType;

    [Header("Upgrade Stats")]
    [SerializeField] private int speedUpgradeAmount = 20;
    [SerializeField] private int maxHealtUpgradeAmount = 30;
    [SerializeField] private int damageUpgradeAmount = 47;

    [Header("Coast ")]
    [SerializeField] private int coast= 20;

    [Header("UI Poin")]
    [SerializeField] private TMP_Text currentPoint;
    [SerializeField] private TMP_Text pointText;

    [Header("Timer Reference")]
    [SerializeField] private Timer gameTimer;

    private ScoreManager poinScore;
    public GameObject displayUpgradeStat;
    private Button buttonComponent;
    private Player playerStat;
    private int currentCost;
    private bool isUpgradeStatOpen = false;
    
    void Awake()
    {
        buttonComponent = GetComponent<Button>();
        currentCost = coast;
        playerStat = FindAnyObjectByType<Player>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Escape))
        {
            if (isUpgradeStatOpen == true)
            { 
                CloseUpgradeStat();
            }
            else
            {
                TriggerUpdateStat();
            }
        }
    }

    private void OnEnable()
    {
        CurrentPoint();
    }

    public void UpgradeStat()
    {
        if (playerStat == null || ScoreManager.instance == null) return;
        if (ScoreManager.instance.CurrentPoint < currentCost) return;
        
        ScoreManager.instance.UpdatePoint(-currentCost);
        switch (upgradeType)
        {
            case UpgradeType.MoveSpeed:
                playerStat.MoveSpeed += speedUpgradeAmount;
                break;
            case UpgradeType.MaxHealth:
                playerStat.MaxHealth += maxHealtUpgradeAmount;
                break;
            case UpgradeType.Damage:
                playerStat.AttackDamage += damageUpgradeAmount;
                break;
        }
        UpdatePointUI();
        CurrentPoint();
    }

    public void CurrentPoint()
    {
        if (ScoreManager.instance != null && pointText != null)
        {
            pointText.text = "Current Poin: " + ScoreManager.instance.CurrentPoint;
        }

    }

    private void UpdatePointUI()
    {
        if (pointText != null)
        {
            pointText.text = "Point : " + currentCost;
        }
    }

    public void TriggerUpdateStat()
    {
        isUpgradeStatOpen = true;
        if (displayUpgradeStat != null) displayUpgradeStat.SetActive(true); 
        Time.timeScale = 0f; // Pause the game
    }

    public void CloseUpgradeStat()
    {
        isUpgradeStatOpen = false;
        if (displayUpgradeStat != null) displayUpgradeStat.SetActive(false);
        Time.timeScale = 1f;
        if (gameTimer != null)
        {
            gameTimer.ResetTimer();
        }
        
    }
}
