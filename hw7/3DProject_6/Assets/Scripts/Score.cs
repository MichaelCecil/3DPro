using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text text;
    private int score;

    private void Start()
    {
        score = 0;
    }

    public void addScore()
    {
        score++;
        setTextContent();
    }

    void setTextContent()
    {
        text.text = "Score: " + score.ToString();
        Debug.Log(text.text);
    }

    public void stop()
    {
        text.text = "game over!";
        text.fontSize = 20;
    }
}
