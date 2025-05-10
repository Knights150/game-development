using UnityEngine;
using UnityEngine.SceneManagement;

public enum Difficulty
{
    Easy,
    Normal,
    Hard
}

public static class DifficultyManager
{
    public static Difficulty CurrentDifficulty = Difficulty.Normal;

    public static void SetDifficulty(int index)
    {
        CurrentDifficulty = (Difficulty)index;
        PlayerPrefs.SetInt("Difficulty", index);
        PlayerPrefs.Save();

        Time.timeScale = 1f; // Unpause if game was paused
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadDifficulty()
    {
        int saved = PlayerPrefs.GetInt("Difficulty", 1); // Default to Normal
        CurrentDifficulty = (Difficulty)saved;
    }
}
