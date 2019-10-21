using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 mousePosition, direction;
    public float speed = 10f;

    private void Start()
    {
        direction = transform.position;
    }

    private void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.CompareTag("plane"))
            {
                direction = hit.point;
                direction = direction + new Vector3(0, transform.position.y, 0);
            }
        }
        if(transform.position != direction)
        {
            transform.position = Vector3.MoveTowards(transform.position, direction, Time.deltaTime * speed);
        }
    }
}
