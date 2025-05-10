using UnityEngine;

public static class SettingsManager
{
    public static float MasterVolume = 1f;
    public static float MusicVolume = 0.5f;
    public static float SFXVolume = 0.5f;
    public static int ResolutionIndex = 0;

    public static void LoadSettings()
    {
        MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        ResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);
    }

    public static void SaveSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", MasterVolume);
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
        PlayerPrefs.SetFloat("SFXVolume", SFXVolume);
        PlayerPrefs.SetInt("ResolutionIndex", ResolutionIndex);
        PlayerPrefs.Save();
    }
}
