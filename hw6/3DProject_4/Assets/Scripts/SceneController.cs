using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    ArrowController ac;
    SSDirector ssDirector;
    public Text Score;
    private int score;
    private int curScore;

    private void Awake()
    {
        ssDirector = SSDirector.getInstance();
        ssDirector.setFPS(30);
        ssDirector.sceneController = this;
        ssDirector.sceneController.LoadResources();
        score = 0;
    }

    private void LoadResources()
    {
        ac = Singleton<ArrowController>.instance;
        ac.initController();
    }

    private void Update()
    {
        ac.initArrow();
        ac.RecycleArrow();
        curScore = ac.getScore();
        if(curScore > 0)
        {
            score += curScore;
            setTextContent();
        }
    }

    private void FixedUpdate()
    {
        ac.RunArrow();
    }

    void setTextContent()
    {
        print("Attacked! this turn's score is: " + curScore);
        Score.text = "Score: " + score.ToString();
    }
}
