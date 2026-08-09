using UnityEngine;

public class Character : MonoBehaviour
{
    // health, damage
    [Header("Base Attributes")]
    [SerializeField] private string characterName;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private int attackDamage = 15;

    // Default Constructor
    public Character()
    {
        characterName = "Unnamed";
        maxHealth = 100;
        currentHealth = 100;
        attackDamage = 15;
    }

    // Parameterized Constructor
    public Character(string characterName, int maxHealth, int attackDamage)
    {
        this.characterName = characterName;
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.attackDamage = attackDamage;
    }

    // Public Getters and Setters
    public string CharacterName
    {
        get => characterName;
        set => characterName = value;
    }

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = Mathf.Max(1, value);
    }

    public int CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = Mathf.Clamp(value, 0, maxHealth);
    }

    public int AttackDamage
    {
        get => attackDamage;
        set => attackDamage = Mathf.Max(0, value);
    }


}
