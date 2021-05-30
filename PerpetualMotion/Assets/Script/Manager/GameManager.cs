﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    /// <summary>シーンの名前 </summary>
    public string SceneName;
    /// <summary>クリアの時間 </summary>
    public int clearTime = 0;
    /// <summary> </summary>
    public int killedEnemy = 0;
    /// <summary> </summary>
    public int gimmickCount = 0;
    /// <summary> </summary>
    [SerializeField] Text[] textBox;
    /// <summary> </summary>
    [SerializeField] GameObject itemPrefabBase = null;
    /// <summary> </summary>
    NavMeshHit hit;
    /// <summary> 参照先の変数 </summary>
    GameMessenger gameMessenger;
    /// <summary> 参照先の変数 </summary>
    PlayerController m_playerController;
    /// <summary> AudioManagerを参照する変数 </summary>
    AudioManager audioManager;

    void Start()
    {
        m_playerController = FindObjectOfType<PlayerController>();

        if (!gameMessenger)
        {
            if (GameObject.FindObjectOfType<GameMessenger>())
            {
                gameMessenger = GameObject.FindObjectOfType<GameMessenger>();
                gameMessenger.mobiusScript.gameManager = this;
            }
        }

        if (GameObject.FindObjectOfType<AudioManager>())
        {
            audioManager = GameObject.FindObjectOfType<AudioManager>();
        }
    }

    public void LostItem(Vector3 position, GameObject itemObj)
    {
        var item = Instantiate(itemObj, new Vector3(position.x + Random.Range(-1, 1), position.y + Random.Range(-1, 1), 0), new Quaternion(0, 0, 0, 0));
        Debug.Log(item.transform.position);
        if (NavMesh.SamplePosition(item.transform.position, out hit, 2.0f, NavMesh.AllAreas))
        {
            item.transform.position = hit.position;
        }
    }

    public void playerPullback(GameObject gameObj)
    {
        if (NavMesh.SamplePosition(gameObj.transform.position, out hit, 2.0f, NavMesh.AllAreas))
        {
            gameObj.transform.position = hit.position;
        }
    }

    public void FitStart(int item)
    {
        StartCoroutine(FitPiece(item));
    }

    IEnumerator FitPiece(int item)
    {
        gameMessenger.mobius.SetActive(true);
        yield return new WaitForSeconds(1f);
        if (gameMessenger.mobiusScript.FitPiece(item))
        {
            
        }
        else
        {
            yield return new WaitForSeconds(1f);
            gameMessenger.mobius.SetActive(false);
        }

    }

    public void OpenResult(bool isClear)
    {
        if (isClear)
        {
            StartCoroutine(ClearResult());
        }
        else
        {
            gameMessenger.gameOverResult.SetActive(true);
            //ゲームオーバーBGM
            audioManager.PlayBgm(audioManager.gameOverBGM);
        }
    }

    IEnumerator ClearResult()
    {
        m_playerController.m_active = false;
        gameMessenger.director.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        gameMessenger.stageClearAnim.SetActive(true);
        yield return new WaitForSeconds(4f);
        gameMessenger.clearResult.SetActive(true);
        /*textBox[0].text = clearTime.ToString();
        textBox[1].text = killedEnemy.ToString();
        textBox[2].text = gimmickCount.ToString();*/
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
