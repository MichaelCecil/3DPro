using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : Object
{
    public delegate void playerEH(Vector3 position);
    public event playerEH playerE;

    public delegate void scoreEH();
    public event scoreEH scoreE;

    public void forPlayer(Vector3 position)
    {
        playerE?.Invoke(position);
    }

    public void forScore()
    {
        scoreE?.Invoke();
    }
}
