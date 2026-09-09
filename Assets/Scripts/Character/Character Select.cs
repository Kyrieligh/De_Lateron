using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character_Select: MonoBehaviour
{
    public GameObject[] characters;
    public int selectedCharacter = 0;
    
    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        SceneManager.LoadScene(2);
    }
}
