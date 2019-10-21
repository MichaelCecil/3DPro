using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private Singleton() { }
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
                if (_instance == null)
                {
                    Debug.LogError("无法找到对应单例：" + typeof(T) + " ，查看该单例是否被加载。");
                }
            }
            return _instance;
        }
    }
}

