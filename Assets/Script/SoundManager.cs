using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioMixer mixer;
    public AudioSource bgSound;

    void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        if (PlayerPrefs.HasKey("BGM"))
            BgSoundVolume(PlayerPrefs.GetFloat("BGM"));

        if (PlayerPrefs.HasKey("SFX"))
            SFXVolume(PlayerPrefs.GetFloat("SFX"));
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];

        audioSource.clip = clip;
        audioSource.Play();

        Destroy(go, clip.length);
    }

    public void BgSoundVolume(float val)
    {
        if (val == -40f)
        {
            mixer.SetFloat("BGM", -80); // 볼륨 조절
            PlayerPrefs.SetFloat("BGM", -80);
        }
        else
        {
            mixer.SetFloat("BGM", val);
            PlayerPrefs.SetFloat("BGM", val);
        }
    }

    public void SFXVolume(float val)
    {
        if (val == -40f)
        {
            mixer.SetFloat("SFX", -80); // 볼륨 조절
            PlayerPrefs.SetFloat("SFX", -80);
        }
        else
        {
            mixer.SetFloat("SFX", val);
            PlayerPrefs.SetFloat("SFX", val);
        }
    }

    public void BgSoundPlay()
    {
        bgSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0];
        bgSound.loop = true;
        bgSound.volume = 0.1f;
        bgSound.Play();
    }
}
