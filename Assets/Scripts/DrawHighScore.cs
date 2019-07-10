using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawHighScore : MonoBehaviour
{

    void Start()
    {
        DrawHighScoreOnTextcomponent();
    }

    //programski kod koji zapisuje 
    private void DrawHighScoreOnTextcomponent()
    {
        int scoreToDraw = GameObject.Find("HighScoreKeeper").GetComponent<HighScore>().GetHighScore();
        Text scoreText = gameObject.GetComponent<Text>();

        scoreText.text = scoreToDraw.ToString();
    }
}
