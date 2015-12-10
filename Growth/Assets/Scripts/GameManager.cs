﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    //=======Singleton implementation=======//
    private static GameManager _instance;
    public static GameManager instance
    {
        get { return _instance ?? (_instance = new GameObject("GameManager").AddComponent<GameManager>()); }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            if (this != _instance)
                Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private int m_iNumEnemies;
    private int m_icurrentLevel = 1;

    public void ResetLevel()
    {
        m_iNumEnemies = 0;
        Application.LoadLevel(Application.loadedLevel);
        PlayerController.instance.ResetLevel();
    }

    private void StartGame()
    {
        Application.LoadLevel("level1");
        PlayerController.instance.ShowSkills();
    }

    public void ReceiveMouseInput(GameObject clicked)
    {
        PlayerController.instance.ReceiveMouseInput(clicked);

        if (clicked.name.Equals("play_button"))
        {
            StartGame();
        }
    }

    public void AddEnemy()
    {
        m_iNumEnemies++;
    }

    public void RemoveEnemy()
    {
        m_iNumEnemies--;
        if (m_iNumEnemies == 0)
        {
            //Load next level
            m_icurrentLevel++;
            Application.LoadLevel("level" + m_icurrentLevel);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}