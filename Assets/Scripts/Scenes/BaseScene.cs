using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseScene : MonoBehaviour
{
    public Enums.Scene SceneType { get; protected set; } = Enums.Scene.Unknown;

    private bool _isInit = false;

    private void Start()
    {
        Init();
    }

    public virtual void Clear() { }

    protected virtual void Init()
    {
        if (_isInit)
        {
            return;
        }

        var eventSystem = FindObjectOfType(typeof(EventSystem));
        if (!eventSystem)
        {
            var go = new GameObject { name = "[EventSystem]" };
            go.AddComponent<EventSystem>();
            go.AddComponent<StandaloneInputModule>();
        }

        _isInit = true;
    }
}
