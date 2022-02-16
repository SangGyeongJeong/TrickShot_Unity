using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSettingButton : MonoBehaviour
{
    public Slider BgSlider;
    public Slider SFXSlider;

    void Awake() 
    {
        if (PlayerPrefs.HasKey("BGM"))
            BgSlider.value = PlayerPrefs.GetFloat("BGM");
        if (PlayerPrefs.HasKey("SFX"))
            SFXSlider.value = PlayerPrefs.GetFloat("SFX");
    }
}
