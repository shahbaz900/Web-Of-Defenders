using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    Button myButton;
    [SerializeField] int level;
    public void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(HandleButtonClick);
    }

    public void HandleButtonClick()
    {
        PlayerPrefs.SetInt("Level", level);

        Debug.Log("Button clicked");
        SceneManager.LoadScene("SampleScene");
    }
}
