using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenuManager : MonoBehaviour
{
    public GameObject endMenuPanel;     // UI panel to show at end
    public Button replayButton;         // Button to replay current level
    public Button restartButton;        // Button to restart from main menu (scene 0)

    void Start()
    {
        // Hide menu on start
        endMenuPanel.SetActive(false);

        // Button click handlers
        replayButton.onClick.AddListener(ReplayLevel);
        restartButton.onClick.AddListener(RestartGame);
    }

    // Call this when player wins
    public void ShowEndMenu()
    {
        Time.timeScale = 0f; // Pause game
        endMenuPanel.SetActive(true);
    }

    // Replay the current level
    void ReplayLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Restart game (load scene 0)
    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // Scene 0 should be your Main Menu or starting scene
    }
}
