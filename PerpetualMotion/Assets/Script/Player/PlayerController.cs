﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : ColorInfo
{
    public float m_moveSpeed = 0.02f;
    float m_stopSpeed;
    public SpriteRenderer playerImage;
    public int playerHp = 5;

    // Start is called before the first frame update
    void Start()
    {
        m_stopSpeed = m_moveSpeed;
        playerImage =  GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame

    void Update()
    {
        if (/*Input.GetKey(KeyCode.LeftArrow) || */Input.GetKey(KeyCode.A))
        {
            transform.Translate(-m_moveSpeed, 0, 0);
        }
        if (/*Input.GetKey(KeyCode.RightArrow) || */Input.GetKey(KeyCode.D))
        {
            transform.Translate(m_moveSpeed, 0, 0);
        }
        if (/*Input.GetKey(KeyCode.UpArrow) || */Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, m_moveSpeed, 0);
        }
        if (/*Input.GetKey(KeyCode.DownArrow) || */Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -m_moveSpeed, 0);
        }

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag== "enemy")
        {
            playerHp -= 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("トリガー" + collision.gameObject.name);
    }
    public void Form_red()
    {
        playerImage.color = Color.red;
    }
    public void Form_green()
    {
        playerImage.color = Color.green;
    }
    public void Form_blue()
    {
        playerImage.color = Color.blue;
    }
    public void mod_craft()
    {
        m_moveSpeed = 0;
    }
    public void mod_mov()
    {
        m_moveSpeed = m_stopSpeed;
    }
}
