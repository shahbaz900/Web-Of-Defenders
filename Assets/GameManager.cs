using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void StartGame()
    {

        SceneManager.LoadScene("LSM");
    }

    public void QuitGame()
    {
       
        Application.Quit();
    }
}
