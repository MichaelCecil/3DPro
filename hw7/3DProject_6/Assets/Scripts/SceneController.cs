using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    SSDirector sDirector;
    NpcController npcController;
    EventController eventController;
    public Score score;

    public Player player;

    List<Npc> npcs;

    private void Start()
    {
        sDirector = SSDirector.getInstance();
        sDirector.setFPS(30);
        sDirector.sceneController = this;
        sDirector.sceneController.LoadResources();
        npcs = npcController.Npcs;

        for (int i = 0; i < npcs.Count; i++)
        {
            eventController.playerE += npcs[i].follow;
        }
        eventController.scoreE += score.addScore;
    }

    private void Update()
    {
        Trace();
        Escape();
        Caught();
    }

    void LoadResources()
    {
        npcController = Singleton<NpcController>.instance;
        npcController.initController();
        eventController = new EventController();
    }

    void Trace()
    {
        eventController.forPlayer(player.transform.position);
    }

    void Escape()
    {
        for(int i = 0; i < npcs.Count; i++)
        {
            if(npcs[i].escape)
            {
                eventController.forScore();
                npcs[i].escape = false;
            }
        }
    }

    void Caught()
    {
        for(int i = 0; i < npcs.Count; i++){
            if (npcs[i].caught)
            {
                Time.timeScale = 0;
                score.stop();
            }
        }
    }
}
