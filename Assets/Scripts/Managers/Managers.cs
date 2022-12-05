using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers s_instance;
    private static Managers GetInstance { get { init(); return s_instance; } }

    public static PoolManager Pool { get { return GetInstance._pool; } }
    public static ResourceManager Resource { get { return GetInstance._resource; } }

    private PoolManager _pool = new();
    private ResourceManager _resource = new();

    private static void init()
    {
        if (!s_instance)
        {
            var managers = FindObjectOfType<Managers>();
            GameObject go = null;
            if (!managers)
            {
                go = new GameObject { name = "[Managers]" };
                go.AddComponent<Managers>();
            }
            else
            {
                go = managers.gameObject;
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }
}
