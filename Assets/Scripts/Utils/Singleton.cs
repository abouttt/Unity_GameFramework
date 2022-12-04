using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T s_instance = null;

    public static T GetInstance
    {
        get
        {
            if (!s_instance)
            {
                s_instance = FindObjectOfType<T>();

                if (!s_instance)
                {
                    var obj = new GameObject { name = $"[{typeof(T).Name}]" };
                    s_instance = obj.AddComponent<T>();
                }
            }

            return s_instance;
        }
    }

    public virtual void Awake()
    {
        if (!s_instance)
        {
            s_instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
