using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMain : MonoBehaviour
{
    Button myButton;
    public void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(HandleButtonClick);
    }

    public void HandleButtonClick()
    {
        Debug.Log("Button clicked");
        SceneManager.LoadScene("MENU");
    }
}
