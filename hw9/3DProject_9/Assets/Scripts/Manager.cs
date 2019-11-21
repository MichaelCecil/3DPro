using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Continue
{
    int _times;
    float _nums;

    public Continue(int t, float n)
    {
        this._times = t;
        this._nums = n;
    }

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

class player
{
    public float health;
    public float MP;
    public float def;
    private float imediate;
    private List<Continue> continues;

    public player(float h, float m, float def)
    {
        this.health = h;
        this.MP = m;
        this.def = def;
        imediate = 0f;
        continues = new List<Continue>();
    }

    public void calculate()
    {
        imediate = (imediate <= def ? 0 : imediate - def);
        health -= imediate;
        if (continues.Count > 0)
        {
            for (int i = 0; i < continues.Count; i++)
            {
                health += continues[i].nums;
                continues[i].times--;
                if (continues[i].times <= 0)
                {
                    continues.RemoveAt(i);
                }
            }
        }

        imediate = 0;
        if (health <= 0)
            health = 0;
    }

    public bool check()
    {
        if (health <= 0)
            return false;
        if (health > 100)
            health = 100;
        if (MP < 0)
            MP = 0;
        if (MP > 100)
            MP = 100;
        return true;
    }

    public float Imediate
    {
        set
        {
            imediate = value;
        }
        get
        {
            return imediate;
        }
    }

    public Continue con
    {
        set
        {
            continues.Add(value);
        }
    }
}

public class Manager : MonoBehaviour
{
    public Button m1;
    public Button m2;
    public Button m3;
    public Button m4;

    public Slider s1;
    public Slider s2;
    public Slider s3;

    public Text text;

    private bool whoseRun;
    private bool isPlay;

    private player p1, p2;
    private List<Button> buttons;

    private void Start()
    {
        text.text = "";
        p1 = new player(100, 50, 0);
        p2 = new player(100, 50, 0);
        whoseRun = true;
        isPlay = true;
        buttons = new List<Button> { m1, m2, m3, m4 };
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(delegate ()
            {
                this.Onclick(button);
            });
        }
    }

    private void Update()
    {
        if (isPlay)
        {
            if (!whoseRun)
            {
                p1.Imediate = 6;
                p1.MP += 5;
                whoseRun = true;
            }
            setHealth();
        }
        else
        {
            text.text = "游戏结束！";
            Time.timeScale = 0;
        }
    }

    void Onclick(Button b)
    {
        bool isAttack = false;
        if (!whoseRun)
            return;
        if (b == m1 && p1.MP >= 0)
        {
            p1.MP -= 0;
            p1.health -= 10;
            p1.MP += 10;
            isAttack = true;
        }
        else if (b == m2 && p1.MP >= 20)
        {
            p1.MP -= 20;
            p2.con = new Continue(5, -2);
            isAttack = true;
        }
        else if (b == m3 && p1.MP >= 40)
        {
            p1.MP -= 40;
            p1.def += 1;
            isAttack = true;
        }
        else if (b == m4 && p1.MP >= 60)
        {
            p1.MP -= 60;
            p1.con = new Continue(10, 1);
            p2.con = new Continue(10, -3);
            isAttack = true;
        }

        if (!isAttack)
            p2.Imediate = 3;

        p1.calculate();
        p2.calculate();

        isPlay = p1.check();
        if (isPlay)
            isPlay = p2.check();
        whoseRun = false;
    }

    void setHealth()
    {
        s1.value = Mathf.Lerp(s1.value, p1.health, 1);
        s2.value = Mathf.Lerp(s2.value, p2.health, 1);
        s3.value = Mathf.Lerp(s3.value, p1.MP, 1);
    }
}
