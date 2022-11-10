using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu instance;
    public AudioMixer audioMixer;

    public Slider masterVolumeSlider;
    public Slider effectsVolumeSlider;
    public Slider musicVolumeSlider;

    public TMP_Dropdown graphicsDropdown;
    public TMP_Dropdown fullscreenDropdown;
    public TMP_Dropdown resolutionDropdown;
    public Toggle vsyncToggle;

    Resolution[] resolutions;
    FullScreenMode currentFullscreenMode;

    public GameObject settingsUI;
    public GameObject controlsUI;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        controlsUI = transform.GetChild(0).gameObject;
        settingsUI = transform.GetChild(1).gameObject;
    }

    private void Start()
    {
        // Accessing saved values

        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        graphicsDropdown.value = PlayerPrefs.GetInt("QualityIndex", 5);
        fullscreenDropdown.value = PlayerPrefs.GetInt("WindowIndex", 0);

        if(PlayerPrefs.GetInt("VSyncIndex", 0) == 1)
        {
            vsyncToggle.isOn = true;
        } 
        else
        {
            vsyncToggle.isOn = false;
        }

        // Resolution stuffs

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height && resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetSoundEffectsVolume(float volume)
    {
        audioMixer.SetFloat("EffectsVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("EffectsVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityIndex", qualityIndex);
    }

    public void SetWindowMode(int windowIndex)
    {
        if (windowIndex == 0)
        {
            currentFullscreenMode = FullScreenMode.ExclusiveFullScreen;
            Screen.fullScreen = true;
            Cursor.lockState = CursorLockMode.Confined;
        } 
        else if (windowIndex == 1)
        {
            currentFullscreenMode = FullScreenMode.FullScreenWindow;
            Screen.fullScreen = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (windowIndex == 2)
        {
            currentFullscreenMode = FullScreenMode.Windowed;
            Screen.fullScreen = false;
            Cursor.lockState = CursorLockMode.None;
        }

        Screen.fullScreenMode = currentFullscreenMode;

        PlayerPrefs.SetInt("WindowIndex", windowIndex);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, currentFullscreenMode);
    }

    public void SetVSync(bool isVSync)
    {
        int vSyncIndex;

        if (isVSync)
        {
            vSyncIndex = 1;
        } 
        else
        {
            vSyncIndex = 0;
        }

        QualitySettings.vSyncCount = vSyncIndex;

        PlayerPrefs.SetInt("VSyncIndex", vSyncIndex);
    }

    public void CloseUI()
    {
        if (settingsUI.activeSelf)
        {
            settingsUI.GetComponent<EnlargePanel>().Shrink();
            //settingsUI.GetComponentInChildren<FadePanel>().Reset();
            settingsUI.SetActive(false);
        }
        if (controlsUI.activeSelf)
        {
            controlsUI.GetComponent<EnlargePanel>().Shrink();
            controlsUI.SetActive(false);
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            MainMenu.instance.ShowMain();
        } 
        else
        {
            //GameManager.instance.Pause();
        }

        Time.timeScale = GameManager.instance.speedMultiplier;
    }

    public void OpenSettings()
    {
        AudioManager.instance.Play("Click");
        CloseUI();
        if (!settingsUI.activeSelf)
        {
            settingsUI.SetActive(true);
            settingsUI.GetComponent<EnlargePanel>().Enlarge();
        }
    }

    public void OpenControls()
    {
        AudioManager.instance.Play("Click");
        CloseUI();
        if (!controlsUI.activeSelf)
        {
            controlsUI.SetActive(true);
            controlsUI.GetComponent<EnlargePanel>().Enlarge();
        }
    }
}
