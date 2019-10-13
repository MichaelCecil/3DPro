using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Arrow : MonoBehaviour
{
    int curScore;
    bool isAttacked;
    bool isStopped;

    private float maxYRotation = 90;
    private float minYRotation = 0;
    private float maxXRotation = 50;
    private float minXRotation = 0;

    private int fullScore;

    public Transform TargetTf;

    public int score
    {
        get
        {
            return curScore;
        }
    }

    public bool attacked
    {
        get
        {
            return isAttacked;
        }
    }

    public bool stopped
    {
        get
        {
            return isStopped;
        }
    }

    private void Start()
    {
        fullScore = (int)TargetTf.position.y;
        isAttacked = false;
        isStopped = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isStopped = true;
        GetComponentInChildren<BoxCollider>().isTrigger = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Destroy(GetComponent<Rigidbody>());
        Vector3 pos = collision.contacts[0].point;
        curScore = fullScore - (int)(TargetTf.position - pos).magnitude;
        if (curScore <= 0 || curScore > 5)
        {
            isAttacked = false;
            curScore = 0;
        }
        else
        {
            isAttacked = true;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float xPosPer = Input.mousePosition.x / Screen.width;
            float yPosPer = Input.mousePosition.y / Screen.height;
            float xAngle = -Mathf.Clamp(yPosPer * maxXRotation, minXRotation, maxXRotation) + 35;
            float yAngle = Mathf.Clamp(xPosPer * maxYRotation, minYRotation, maxYRotation) - 60;
            transform.eulerAngles = new Vector3(xAngle, yAngle, 0);
        }
    }
}