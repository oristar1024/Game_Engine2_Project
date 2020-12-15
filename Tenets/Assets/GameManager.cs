﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int player_turn;
    public int playerScore1;
    public int playerScore2;
    public int boundCount;
    public int servicePlayer;
    public GameObject ball;
    public Transform player1Pos;
    public Transform player2Pos;
    public int player1SetScore;
    public int player2SetScore;
    public ballCtrl con;

    public GameObject winPanel;
    public Text wintext;

    public Text SetScore;
    public Text Score;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        SetServicePos();
        winPanel.SetActive(false);
    }
    // Update is called once per frame

    public void Out(){
        if(boundCount == 0)
        {
            if (player_turn == 0)
            {
                playerScore2 += 1;
                servicePlayer = 0;
            }
            else
            {
                playerScore1 += 1;
                servicePlayer = 1;
            }
        }
        else
        {
            if (player_turn == 0)
            {
                playerScore1 += 1;
                servicePlayer = 1;
            }
            else
            {
                playerScore2 += 1;
                servicePlayer = 0;
            }
        }
        SetServicePos();
    }

    public void Conversion(int player)
    {
        if (player == player_turn)
            con.Conversion(player);
    }

    public void Clear()
    {
        con.Clear();
    }

    void Update()
    {
        if(boundCount > 1)
        {
            if (player_turn == 0)
            {
                playerScore1 += 1;
                servicePlayer = 1;
            }
            else
            {
                playerScore2 += 1;
                servicePlayer = 0;
            }
            SetServicePos();
        }

        if(playerScore1 > 3)
        {
            player1SetScore += 1;
            init();
        }
        else if(playerScore2 > 3)
        {
            player2SetScore += 1;
            init();
        }

        if(player2SetScore > 2)
        {
            winPanel.SetActive(true);
            wintext.text = "P2 Win!";
            Time.timeScale = 0;
        }
        else if(player1SetScore > 2)
        {
            winPanel.SetActive(true);
            wintext.text = "P1 Win!";
            Time.timeScale = 0;
        }
    }

    public void Retry()
    {
        player1SetScore = 0;
        player2SetScore = 0;
        playerScore1 = 0;
        playerScore2 = 0;
        servicePlayer = 0;
        Time.timeScale = 1;
        winPanel.SetActive(false);
        SetServicePos();
    }

    void SetServicePos()
    {
        boundCount = 0;
        SetScoreText();
        con.init();
        if(servicePlayer == 0)
        {
            player_turn = 1;
            if(playerScore1 % 2 == 0)
            {
                player1Pos.position = new Vector3(-35, 2, -5);
                player2Pos.position = new Vector3(35, 2, 0);
                ball.transform.position = new Vector3(-30, 15, -10);
                ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
            else
            {
                player1Pos.position = new Vector3(-35, 2, 5);
                player2Pos.position = new Vector3(35, 2, 0);
                ball.transform.position = new Vector3(-30, 15, 10);
                ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
        }
        else
        {
            player_turn = 0;
            if(playerScore2 % 2 == 0)
            {
                player1Pos.position = new Vector3(-35, 2, 0);
                player2Pos.position = new Vector3(35, 2, 5);
                ball.transform.position = new Vector3(30, 15, 10);
                ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
            else
            {
                player1Pos.position = new Vector3(-35, 2, 0);
                player2Pos.position = new Vector3(35, 2, -5);
                ball.transform.position = new Vector3(30, 15, -10);
                ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
        }
    }

    void SetScoreText()
    {
        int p1 = playerScore1 * 15;
        int p2 = playerScore2 * 15;
        if (p1 > 40)
            p1 = 40;
        if (p2 > 40)
            p2 = 40;
        Score.text = p1 + " : " + p2;
        SetScore.text = player1SetScore + " : " + player2SetScore;
    }

    void init()
    {
        playerScore1 = 0;
        playerScore2 = 0;
        player_turn = 0;
        servicePlayer = 0;
        SetServicePos();
        SetScoreText();
    }
}
