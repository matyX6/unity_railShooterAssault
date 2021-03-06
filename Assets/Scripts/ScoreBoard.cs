﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

    private int score;
    private Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
    }

    public void ScoreHit(int scoreIncrease)
    {
        score = score + scoreIncrease;
        scoreText.text = score.ToString();

        GameObject.Find("HighScoreKeeper").GetComponent<HighScore>().SetHighScore(score);
    }
}
