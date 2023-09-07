using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionManager : MonoBehaviour
{
    public void LoadLevel(int levelIndex)
    {
        
        SceneManager.LoadScene("Level" + levelIndex);
    }

    public void GoBackToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("Menu");
    }
}
