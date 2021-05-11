using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using System.Collections.Generic;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel = null;

    
    [SerializeField] private AudioMixer audioMixer = null;

    [SerializeField] private TMP_Dropdown resolutionDropdown = null;
    private Resolution[] resolutions;

    private void Start()
    {
        SettingResolution();
    }

    private void SettingResolution()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        int currentResoltuionIndex = 0;
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResoltuionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResoltuionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void OpenSettingPanel()
    {
        settingPanel.SetActive(true);
    }

    public void CloseSettingPanel()
    {
        settingPanel.SetActive(false);
    }

    public void SetSoundsVolume(float volume)
    {
        audioMixer.SetFloat("MainSound", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicSound", volume);
    }

    public void SetSfxVolume(float volume)
    {
        audioMixer.SetFloat("SfxSound", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
