using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_OptionSettings : MonoBehaviour
{
    
    public Dropdown _resolutionDropdown;
    
    Resolution[] resolutions;

    private void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        
        resolutions = Screen.resolutions;
        
        _resolutionDropdown.ClearOptions();
        
        //Allow to get the resolutions
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        } 
        //And with them set things up
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    //Allow to set resolution and change it to the size chosen
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
    //Allow to change the quality
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //Allow to make the game full screen
    public void FullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

}
