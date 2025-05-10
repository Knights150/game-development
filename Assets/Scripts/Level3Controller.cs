using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level3Controller : MonoBehaviour
{
    public float timeToSurvive = 20f;
    public Text timerText;
    private bool isGameOver = false;

    void Update()
    {
        if (isGameOver) return;

        timeToSurvive -= Time.deltaTime;
        timerText.text = "Survive: " + Mathf.Ceil(timeToSurvive) + "s";

        if (timeToSurvive <= 0f)
        {
            WinLevel();
        }
    }

    void WinLevel()
    {
        isGameOver = true;
        Debug.Log("You survived!");
        // Optional: Show win screen
    }

    public void LoseLevel()
    {
        isGameOver = true;
        Debug.Log("You crashed! Game Over");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart scene
    }
}
