using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    Vector3 nextPosition, priPosition;
    public float norSpeed = 5f;
    public float traSpeed = 7f;
    public static float TraceDistance = 15f;
    public static float escapeDistance = 20f;
    public static float caughtDistance = 2f;
    bool isTrace;
    public bool escape { set; get; }
    public bool caught { set; get; }

    private void Start()
    {
        isTrace = false;
        escape = false;
        caught = false;
        getNextPosition();
        priPosition = transform.position;
    }

    private void Update()
    {
        if (!isTrace)
        {
            patrol();
            if (Vector3.Distance(transform.position, priPosition) < norSpeed * Time.deltaTime / 3)
            {
                getNextPosition();
            }
            priPosition = transform.position;
        }
    }

    void patrol()
    {
        if (transform.position == nextPosition)
        {
            getNextPosition();
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, Time.deltaTime * norSpeed);
    }

    public void follow(Vector3 position)
    {
        float distance = Vector3.Distance(transform.position, position);
        if (!isTrace && distance < TraceDistance)
        {
            isTrace = true;
            escape = false;
        }
        else if(isTrace && distance > escapeDistance)
        {
            isTrace = false;
            escape = true;
        }
        else if(isTrace && distance < caughtDistance)
        {
            caught = true;
        }

        if(isTrace)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * traSpeed);
        }
    }

    void getNextPosition()
    {
        float xPosition = Random.Range(-5f, 5f);
        if(xPosition < 0)
            xPosition -= 3f;
        else
            xPosition += 3f;

        float zPosition = Random.Range(-5f, 5f);
        if (zPosition < 0)
            zPosition -= 3f;
        else
            zPosition += 3f;

        nextPosition = new Vector3(xPosition, 0, zPosition) + transform.position;
        nextPosition.y = 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        getNextPosition();
    }
}
