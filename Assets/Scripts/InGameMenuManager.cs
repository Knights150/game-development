using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class InGameMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject menuPanel;

    [Header("Buttons")]
    public Button masterButton, musicButton, sfxButton, resumeButton;

    [Header("Sliders")]
    public Slider masterSlider, musicSlider, sfxSlider;

    [Header("Dropdowns")]
    public TMP_Dropdown difficultyDropdown;

    [Header("Audio")]
    public AudioMixer audioMixer; // Assign this in the Inspector (your AudioMixer asset)

    private bool menuVisible = false;

    void Start()
    {
        // Load and apply difficulty
        DifficultyManager.LoadDifficulty();
        difficultyDropdown.value = (int)DifficultyManager.CurrentDifficulty;
        difficultyDropdown.onValueChanged.AddListener(DifficultyManager.SetDifficulty);

        // Set up volume slider listeners
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        // Optional: set sliders to default values (e.g., 1f)
        masterSlider.value = 1f;
        musicSlider.value = 1f;
        sfxSlider.value = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        menuVisible = !menuVisible;
        menuPanel.SetActive(menuVisible);
        Time.timeScale = menuVisible ? 0f : 1f;

        masterSlider.gameObject.SetActive(false);
        musicSlider.gameObject.SetActive(false);
        sfxSlider.gameObject.SetActive(false);
    }

    public void ShowMasterSlider()
    {
        masterSlider.gameObject.SetActive(true);
        musicSlider.gameObject.SetActive(false);
        sfxSlider.gameObject.SetActive(false);
    }

    public void ShowMusicSlider()
    {
        musicSlider.gameObject.SetActive(true);
        masterSlider.gameObject.SetActive(false);
        sfxSlider.gameObject.SetActive(false);
    }

    public void ShowSFXSlider()
    {
        sfxSlider.gameObject.SetActive(true);
        masterSlider.gameObject.SetActive(false);
        musicSlider.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        ToggleMenu();
    }

    // 🔊 Volume Control Methods (use Log10 conversion for dB scale)
    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f);
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f);
    }

    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f);
    }
}
