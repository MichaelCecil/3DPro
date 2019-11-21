using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class continued
{
    int _times;
    float _nums;

    public int times
    {
        set
        {
            _times = value;
        }
        get
        {
            return _times;
        }
    }

    public float nums
    {
        set
        {
            _nums = value;
        }
        get
        {
            return _nums;
        }
    }
}

public class IM_test : MonoBehaviour
{
    public float health = 1.0f;
    public Slider healthSlider;

    private float resultHealth;

    private Rect healthBar;
    private Rect up;
    private Rect down;

    private float imediate;
    private List<continued> last;

    private void Start()
    {
        healthBar = new Rect(50, 50, 100, 20);
        up = new Rect(105, 80, 40, 20);
        down = new Rect(155, 80, 40, 20);

        resultHealth = health;
    }

    private void OnGUI()
    {
        if (GUI.Button(up, "加血"))
        {
            resultHealth += 0.1f;
            if (1.0f - resultHealth <= 0.001f)
            {
                resultHealth = 1.0f;
            }
        }

        if (GUI.Button(down, "减血"))
        {
            resultHealth -= 0.1f;
            if (resultHealth - 0.0f <= 0.01f)
            {
                resultHealth = 0.0f;
            }
        }



        health = Mathf.Lerp(health, resultHealth, 0.05f);
        healthSlider.value = health;

        GUI.HorizontalScrollbar(healthBar, 0.0f, health, 0.0f, 1.0f);
    }
}
