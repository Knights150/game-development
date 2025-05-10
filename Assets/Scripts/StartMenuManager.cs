using UnityEngine;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    public GameObject menuPanel;
    public Button resumeButton;

    private bool isMenuOpen = false;

    void Start()
    {
        menuPanel.SetActive(false);
        resumeButton.onClick.AddListener(CloseMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!isMenuOpen)
                OpenMenu();
        }
    }

    void OpenMenu()
    {
        isMenuOpen = true;
        Time.timeScale = 0f;
        menuPanel.SetActive(true);
    }

    void CloseMenu()
    {
        isMenuOpen = false;
        Time.timeScale = 1f;
        menuPanel.SetActive(false);
    }
}
