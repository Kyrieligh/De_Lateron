using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character_Select: MonoBehaviour
{
    public CharacterDatabase characterDB;
    public SpriteRenderer spriteRenderer;

    private int selectedCharacter = 0;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("selectOption"))
        {
            Load();
        }


    }

    void SelectCharacter(int characterIndex)
    {
        if (characterDB == null || characterIndex < 0 || characterIndex >= characterDB.CharacterCount)
            return;
        selectedCharacter = characterIndex;
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        PlayerPrefs.Save();

        UpdateCharacter(characterIndex);

    }

    private void UpdateCharacter(int index)
    { 
        if (spriteRenderer != null && characterDB != null)
        {
            spriteRenderer.sprite = characterDB.GetCharacter(index).characterSprite;
        }
    }

    private void Load()
    {
        selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");

    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
    
}
