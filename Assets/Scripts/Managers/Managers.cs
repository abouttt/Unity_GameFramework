using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers s_instance;
    private static Managers GetInstance { get { init(); return s_instance; } }

    public static PoolManager Pool { get { return GetInstance._pool; } }
    public static ResourceManager Resource { get { return GetInstance._resource; } }
    public static SceneManagerEx Scene { get { return GetInstance._scene; } }
    public static SoundManager Sound { get { return GetInstance._sound; } }
    public static UIManager UI { get { return GetInstance._ui; } }

    private PoolManager _pool = new();
    private ResourceManager _resource = new();
    private SceneManagerEx _scene = new();
    private SoundManager _sound = new();
    private UIManager _ui = new();

    private void Awake()
    {
        init();
    }

    public static void Clear()
    {
        init();

        Pool.Clear();
        Sound.Clear();
        UI.Clear();
    }

    private static void init()
    {
        if (!s_instance)
        {
            s_instance = FindObjectOfType<Managers>();
            if (!s_instance)
            {
                var go = new GameObject { name = "[Managers]" };
                s_instance = go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(s_instance.gameObject);

            s_instance._pool.Init();
            s_instance._sound.Init();
            s_instance._ui.Init();
        }
    }
}
