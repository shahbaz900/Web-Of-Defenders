using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausemenuScript : MonoBehaviour
{
    public void onResumePressed()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void onExitLevel()
    {
        SceneManager.LoadScene("MENU");
    }
}
