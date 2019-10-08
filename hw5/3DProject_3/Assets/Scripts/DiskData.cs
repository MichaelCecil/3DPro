using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruler
{
    public float size;
    public Color color;
    public int score;
    public Vector3 position;
    public Vector3 direction;
    public float speed;

    public Ruler(float size, Color color, Vector3 position, Vector3 direction, float speed)
    {
        this.size = size;
        this.color = color;
        this.position = position;
        this.direction = direction;
        this.speed = speed;
        this.score = (int)(this.speed + this.size);
    }
}

public class DiskData : MonoBehaviour
{
    Ruler _ruler;
    public Ruler ruler
    {
        get
        {
            return _ruler;
        }
        set
        {
            _ruler = value;
            this.gameObject.GetComponent<Transform>().position = value.position;
            this.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1) * value.size;
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = value.color;
        }
    }
}
