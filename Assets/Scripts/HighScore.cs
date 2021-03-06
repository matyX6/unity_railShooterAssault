﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

    private int highScore = 0;
    private Text highScoreText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetHighScore(int score)
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("hs", score);
        }
    }

    public int GetHighScore()
    {
        highScore = PlayerPrefs.GetInt("hs", 0);
        return highScore;
    }
}
