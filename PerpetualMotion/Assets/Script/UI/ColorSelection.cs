﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelection : MonoBehaviour
{
    public int selectNumber = 0;
    [SerializeField] Slider Select_red;
    [SerializeField] Slider Select_green;
    [SerializeField] Slider Select_blue;
    /// <summary>3本のゲージの値</summary>
    float[] UsageTimes = new float[3]{100f,100f,100f, };
    /// <summary>現在値が0でないゲージの数</summary>
    int shiftCount  = 3;
    /// <summary>ゲージの減少速度</summary>
    public float speed;
    PlayerController playerController;

    void Start()
    {
        Select_red = GameObject.Find("Red").GetComponent<Slider>();
        Select_green = GameObject.Find("Green").GetComponent<Slider>();
        Select_blue = GameObject.Find("Blue").GetComponent<Slider>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerController.Form_red();
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            for (int i = selectNumber - 1; i >= 0; i--)
            {
                if (UsageTimes[i] > 0)
                {
                    selectNumber = i;
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            for (int i = selectNumber + 1; i < UsageTimes.Length; i++)
            {
                if (UsageTimes[i] > 0)
                {
                    selectNumber = i;
                    break;
                }
            }
        }

        if (shiftCount > 0)
        {
            selectColor();
        }
        else//ゲージがすべてなくなったらプレイヤーを白に
        {
            if (playerController.nowColor != ColorInfo.COLOR_TYPE.Blank)
            {
                playerController.Form_blank();
            }
        }
    }
    void selectColor()
    {
        switch (selectNumber)
        {
            case 0:
                if (playerController.nowColor != ColorInfo.COLOR_TYPE.Red)
                {
                    playerController.Form_red();
                }
                if (UsageTimes[0] > 0)
                {
                    UsageTimes[0] -= Time.deltaTime * speed;
                    Select_red.value = UsageTimes[0];
                }
                else//ゲージがなくなったら合図をだす
                {
                    shiftColor();
                    shiftCount--;
                }
                break;
            case 1:
                if (playerController.nowColor != ColorInfo.COLOR_TYPE.Green)
                {
                    playerController.Form_green();
                }
                if (UsageTimes[1] > 0)
                {
                    UsageTimes[1] -= Time.deltaTime * speed;
                    Select_green.value = UsageTimes[1];
                }
                else//ゲージがなくなったら合図をだす
                {
                    shiftColor();
                    shiftCount--;
                }
                break;
            case 2:
                if (playerController.nowColor != ColorInfo.COLOR_TYPE.Bule)
                {
                    playerController.Form_blue();
                }
                if (UsageTimes[2] > 0)
                {
                    UsageTimes[2] -= Time.deltaTime * speed;
                    Select_blue.value = UsageTimes[2];
                }
                else//ゲージがなくなったら合図をだす
                {
                    shiftColor();
                    shiftCount--;
                }
                break;
            default:
                break;
        }
    }

    /// <summary>残っているゲージを探し、切り替える</summary>
    void shiftColor()
    {
        for(int i = 0; i < UsageTimes.Length; i++)
        {
            if (UsageTimes[i] > 0)
            {
                selectNumber = i;
                break;
            }
        }
    }
}
