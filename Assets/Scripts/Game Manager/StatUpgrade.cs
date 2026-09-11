using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatUpgrade : MonoBehaviour
{

    [Header("Upgrade Stats")]
    [SerializeField] private float speedUpgradeAmount = 1.7f;
    [SerializeField] private int maxHealtUpgradeAmount = 15;
    [SerializeField] private int damageUpgradeAmount = 7;

    [Header("Coast ")]
    [SerializeField] private int healthCost = 20;
    [SerializeField] private int  speedCost = 20;
    [SerializeField] private int damageCost = 20;

    [Header("UI Poin")]
    [SerializeField] private TMP_Text pointText;

    [Header("Upgrade Button")]
    [SerializeField] private Button upgradeHealth;
    [SerializeField] private Button upgradeDamage;
    [SerializeField] private Button upgradeSpeed;

    [Header("Timer Reference")]
    [SerializeField] private Timer gameTimer;
    
    [Header("Player Reference")]
    [SerializeField] private Player playerStat;

    [Header("Enemy Spawn Reference")]
    [SerializeField] private EnemySpawner enemySpawner;

    //private ScoreManager poinScore;
    public GameObject displayUpgradeStat;
    private Button buttonComponent;
    private int currentCost;
    private bool isUpgradeStatOpen = false;
    
    void Awake()
    {
       if (playerStat == null)
        {
            playerStat = FindAnyObjectByType<Player>();
        }
    }

    private void Start()
    {
        if (upgradeHealth != null) upgradeHealth.onClick.AddListener(BuyHealth);
        if (upgradeDamage != null) upgradeDamage.onClick.AddListener(BuyDamage);
        if (upgradeSpeed != null) upgradeSpeed.onClick.AddListener(BuySpeed);
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

    public void BuyHealth()
    {
        if (playerStat == null || ScoreManager.instance == null) return;

        if(ScoreManager.instance.CurrentPoint >= healthCost)
        {
            ScoreManager.instance.UpdatePoint(-healthCost);
            playerStat.MaxHealth += maxHealtUpgradeAmount;
            UpdateCurrentPointUI();
        }
    }

    public void BuyDamage()
    {
        if (playerStat == null || ScoreManager.instance == null) return;
        
        if (ScoreManager.instance.CurrentPoint >= damageCost)
        {
            ScoreManager.instance.UpdatePoint(-damageCost);
            playerStat.AttackDamage += damageUpgradeAmount;
            UpdateCurrentPointUI();
        }
    }

    public void BuySpeed()
    {
        if (playerStat == null || ScoreManager.instance == null) return;

        if(ScoreManager.instance.CurrentPoint >= speedCost)
        {
            ScoreManager.instance.UpdatePoint(-speedCost);
            playerStat.MoveSpeed += speedUpgradeAmount;
            UpdateCurrentPointUI();
        }
    }

    public void UpdateCurrentPointUI()
    {
        if (ScoreManager.instance != null && pointText != null)
        {
            pointText.text = "Current Poin : " + ScoreManager.instance.CurrentPoint;
        }
    }

    public void CurrentPoint()
    {
        if (ScoreManager.instance != null && pointText != null)
        {
            pointText.text = "Current Poin: " + ScoreManager.instance.CurrentPoint;
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
        if (enemySpawner != null)
        {
            enemySpawner.NextLevel();
        }
        if (gameTimer != null)
        {
            gameTimer.ResetTimer();
        }
        
    }
}
