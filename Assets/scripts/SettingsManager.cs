using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] SoundManager soundManager;
    [SerializeField] Image soundsButtonImage;
    [SerializeField] Sprite optionOnSprite;
    [SerializeField] Sprite optionOffSprite;

    [Header("Settings")]
    private bool soundState = true;

    private void Awake()
    {
        soundState = PlayerPrefs.GetInt("sounds", 1) == 1;
    }

    void Start()
    {
        Setup();
    }

    void Update()
    {
        
    }

    private void Setup()
    {
        if(soundState)
        {
            EnableSounds();
        }
        else
        {
            DisableSounds();
        }
    }

    public void ChangeSoundsState()
    {
        if (soundState)
        {
            DisableSounds();
        }
        else
        {
            EnableSounds();
        }

        soundState = !soundState;

        int soundSaveState = 0;
        if (soundState)
        {
            soundSaveState = 1;
        }
        else
        {
            soundSaveState = 0;
        }

        PlayerPrefs.SetInt("sounds", soundSaveState);
        //PlayerPrefs.SetInt("sounds", soundState ?  1 : 0);
    }

    private void DisableSounds()
    {
        soundManager.DisableSounds();
        soundsButtonImage.sprite = optionOffSprite;
    }

    private void EnableSounds()
    {
        soundManager.EnableSounds();
        soundsButtonImage.sprite = optionOnSprite;

    }
}
