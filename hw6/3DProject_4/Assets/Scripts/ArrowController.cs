using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Arrow _arrow;
    private List<GameObject> used;
    private Arrow arrow;

    private float MinPressure = 30f;
    private float MaxPressure = 1000f;
    private float clear;
    private static Vector3 ExPosition;
    private bool isOver;
    private bool variable;
    private float Count;
    private float interval;

    private Transform ChildTf;
    private Transform ChildTf_2;
    public float speed = 150f;
    public float Pressure;

    public void initController()
    {
        used = new List<GameObject>();
        variable = true;
        initArrow();
        isOver = false;
    }

    public int getScore()
    {
        if(arrow != null && arrow.attacked){
            return arrow.score;
        }
        return -1;
    }

    public void initArrow()
    {
        if (variable)
        {
            arrow = Instantiate(_arrow);
            arrow.gameObject.SetActive(true);
            ChildTf = arrow.transform.GetChild(0).transform;
            ChildTf_2 = ChildTf.GetChild(0).transform;
            variable = false;
            isOver = false;
        }
    }

    public void RecycleArrow()
    {
        if(arrow != null && arrow.transform.position.y < -5)
        {
            Destroy(arrow.gameObject);
            isOver = false;
            variable = true;
        }
        if (arrow != null && arrow.stopped)
        {
            used.Add(arrow.gameObject);
            Destroy(arrow);
            isOver = false;
            variable = true;
        }
        if(interval < 30f)
        {
            interval += Time.deltaTime;
        }
        else
        {
            for(int i = 0; i < used.Count; i++)
            {
                Destroy(used[i].gameObject);
            }
            used.Clear();
            interval = 0;
        }
    }

    public void RunArrow()
    {
        if(arrow != null)
        {
            Rigidbody rb = arrow.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.drag = 0.5f;
                if (!isOver)
                {
                    if (Input.GetKey(KeyCode.F))
                    {
                        if (Pressure < MaxPressure)
                        {
                            Pressure += Time.deltaTime * speed;
                        }
                        else
                        {
                            Pressure = MaxPressure;
                        }
                    }
                    else
                    {
                        if (Pressure < MinPressure)
                        {
                            Pressure = MinPressure;
                        }
                    }
                    if (Input.GetKeyUp(KeyCode.F))
                    {
                        clear = Pressure;
                        Pressure = 0;
                        Count = 0;
                        isOver = true;
                        ExPosition = ChildTf_2.position - ChildTf.position;
                        ExPosition.Normalize();
                    }
                }
                else
                {
                    if (Count < 0.5f)
                    {
                        Count += Time.deltaTime;
                        rb.AddForce(ExPosition * clear * (1 - Count / 1f));
                    }
                    rb.AddForceAtPosition(Vector3.down * 9.8f, ChildTf_2.position);
                }
            }
        }
    }
}
