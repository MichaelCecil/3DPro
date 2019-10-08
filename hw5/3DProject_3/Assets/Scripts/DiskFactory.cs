using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour
{
    public DiskData disk;
    public static int num = 8;
    DiskData[] diskList = new DiskData[num];
    static int prepared = num + 1;

    public void initFactory()
    {
        for(int i = 0; i < num; i++)
        {
            diskList[i] = Instantiate(disk);
            diskList[i].gameObject.SetActive(false);
        }
    }

    public bool isPrepared()
    {
        prepared = num + 1;
        for (int i = 0; i < num; i++)
        {
            if (diskList[i].gameObject.activeSelf == false)
            {
                prepared = i;
                break;
            }
        }
        if (prepared < num)
            return true;
        else
            return false;
    }

    public void getDisk(Ruler ruler)
    {
        
        if(prepared < num)
        {
            diskList[prepared].ruler = ruler;
            diskList[prepared].gameObject.SetActive(true);
        }
    }

    public void freeDisk()
    {
        for(int i = 0; i < num; i++)
        {
            if(diskList[i].gameObject.transform.position.y < -3)
            {
                diskList[i].gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                diskList[i].gameObject.SetActive(false);
            }
        }
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
