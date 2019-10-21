using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public Npc _npc;
    List<Npc> npcs = new List<Npc>(6);
    List<Vector3> positions = new List<Vector3> { new Vector3(13, 1, 11), new Vector3(-13, 1, 11), new Vector3(13, 1, -11), new Vector3(-13, 1, -11), new Vector3(10, 1, 10), new Vector3(-10, 1, -10)};

    public void initController()
    {
        for(int i = 0; i < 6; i++)
        {
            Npc npc = Instantiate(_npc);
            npc.gameObject.transform.position = positions[i];
            npc.gameObject.SetActive(true);
            npcs.Add(npc);
        }
    }

    public List<Npc> Npcs
    {
        get
        {
            return npcs;
        }
    }
}
