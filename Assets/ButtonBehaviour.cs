using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    public void PauseButton()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }

    public void NextRoundButton()
    {

    }
}
