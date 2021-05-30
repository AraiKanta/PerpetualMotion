﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    /// <summary>セーブマネージャーを格納する場所</summary>
    SaveManager saveManager;
    /// <summary>オーディオマネージャーを格納する場所</summary>
    AudioManager audioManager;
    [SerializeField] Slider sliderBGM;
    [SerializeField] Slider sliderSE;
    /// <summary>パターン1の選択中を表すUI群</summary>
    [SerializeField] GameObject[] selectingUI1;
    /// <summary>パターン2の選択中を表すUI群</summary>
    [SerializeField] GameObject[] selectingUI2;


    void Start()
    {
        if (GameObject.FindObjectOfType<SaveManager>())
        {
            saveManager = GameObject.FindObjectOfType<SaveManager>().GetComponent<SaveManager>();
            Debug.Log(saveManager.saveData.m_BGMVolume);
            Debug.Log(saveManager.saveData.m_SEVolume);
            sliderBGM.value = saveManager.saveData.m_BGMVolume;
            sliderSE.value = saveManager.saveData.m_SEVolume;
            PatternClick(saveManager.saveData.m_inputPatten);

        }
        if (GameObject.FindObjectOfType<AudioManager>())
        {
            audioManager = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        }
    }

    public void VolumeValueChanged(bool isBGM)
    {
        if (isBGM)
        {
            if (audioManager)
            {
                audioManager.VolumeChangeBGM(sliderBGM.value);
            }
            if (saveManager)
            {
                saveManager.saveData.m_BGMVolume = sliderBGM.value;
            }
        }
        else
        {
            if (audioManager)
            {
                audioManager.VolumeChangeSE(sliderSE.value);
            }
            if (saveManager)
            {
                saveManager.saveData.m_SEVolume = sliderSE.value;
            }
        }
    }


    public void PatternClick(bool isPattern1)
    {
        if (!isPattern1)
        {
            foreach(GameObject obj in selectingUI1)
            {
                obj.SetActive(true);
            }
            foreach(GameObject obj in selectingUI2)
            {
                obj.SetActive(false);
            }
            InputChanger.m_inputPattern = false;
            if (saveManager)
            {
                saveManager.saveData.m_inputPatten = false;
            }
        }
        else
        {
            foreach (GameObject obj in selectingUI1)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in selectingUI2)
            {
                obj.SetActive(true);
            }
            InputChanger.m_inputPattern = false;
            if (saveManager)
            {
                saveManager.saveData.m_inputPatten = true;
            }
        }
    }
}
