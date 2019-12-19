using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VirtualButton : MonoBehaviour, IVirtualButtonEventHandler
{
    public GameObject vb;
    public Animator ani;

    void Start()
    {
        VirtualButtonBehaviour vbb = vb.GetComponent<VirtualButtonBehaviour>();
        if (vbb)
        {
            vbb.RegisterEventHandler(this);
        }
    }

    public void OnButtonPressed(VirtualButtonBehaviour vbb)
    {
        ani.SetTrigger("Run");
        Debug.Log("按钮按下");
    }

    public void OnButtonReleased(VirtualButtonBehaviour vbb)
    {
        ani.SetTrigger("Walk");
        Debug.Log("按钮释放");
    }
}
