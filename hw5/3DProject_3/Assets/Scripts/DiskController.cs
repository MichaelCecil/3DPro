using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskController : MonoBehaviour
{
    public DiskData disk;
    public int num = 8;
    DiskData[] diskList;
    Queue<DiskData> pres = new Queue<DiskData>();
    DiskData prepared;

    public void initController()
    {
        diskList = new DiskData[num];
        for (int i = 0; i < num; i++)
        {
            diskList[i] = Instantiate(disk);
            diskList[i].gameObject.SetActive(false);
            pres.Enqueue(diskList[i]);
        }
    }

    public bool isPrepared()
    {
        return pres.Count != 0;
    }

    public void getDisk(Ruler ruler)
    {
        if (isPrepared())
        {
            prepared = pres.Dequeue();
            prepared.ruler = ruler;
            prepared.gameObject.SetActive(true);
        }
    }

    public void freeDisk()
    {
        for(int i = 0; i < num; i++)
        {
            if(diskList[i].gameObject.activeSelf == true && diskList[i].gameObject.transform.position.y < -3)
            {
                diskList[i].gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                diskList[i].gameObject.SetActive(false);
                pres.Enqueue(diskList[i]);
            }
        }
    }

    public void freeDisk(DiskData data)
    {
        data.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        data.gameObject.SetActive(false);
        pres.Enqueue(data);
    }

    public void runDisk()
    {
        for(int i = 0; i < num; i++)
        {
            if(diskList[i].gameObject.activeSelf == true)
            {
                Rigidbody rb = diskList[i].GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.AddForce(Vector3.down * 9.8f);
                    rb.AddExplosionForce(30f * diskList[i].ruler.speed, diskList[i].ruler.direction, 10);
                }
            }
        }
    }
}
