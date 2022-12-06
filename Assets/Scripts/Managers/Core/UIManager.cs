using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager
{
    public UI_Scene SceneUI { get; private set; }

    private Dictionary<string, UI_Popup> _popupObjects = new();
    private Transform _root = null;

    public void Init()
    {
        var root = GameObject.Find("[UI_Root]");
        if (!root)
        {
            root = new GameObject { name = "[UI_Root]" };
        }

        _root = root.transform;
    }

    public void SetCanvas(GameObject go, int sortOrder)
    {
        var canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;
        canvas.sortingOrder = sortOrder;
    }

    public T InstantiateSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (SceneUI)
        {
            Debug.Log($"[UIManager/InstantiateSceneUI] SceneUI already exist.");
            return null;
        }

        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        var go = Managers.Resource.Instantiate($"UI/Scene/{name}", _root);
        SceneUI = go.GetOrAddComponent<T>();

        return SceneUI as T;
    }

    public void ClearSceneUI()
    {
        if (!SceneUI)
        {
            Debug.Log($"[UIManager/ClearSceneUI] SceneUI no exist.");
            return;
        }

        Object.Destroy(SceneUI.gameObject);
        SceneUI = null;
    }

    public void ShowSceneUI()
    {
        if (!SceneUI)
        {
            Debug.Log($"[UIManager/ShowSceneUI] SceneUI no exist.");
            return;
        }

        SceneUI.gameObject.SetActive(true);
    }

    public void CloseSceneUI()
    {
        if (!SceneUI)
        {
            Debug.Log($"[UIManager/CloseSceneUI] SceneUI no exist.");
            return;
        }

        SceneUI.gameObject.SetActive(false);
    }

    public T InstantiatePopupUI<T>(string name = null, Transform parent = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        var prefab = Managers.Resource.Load<GameObject>($"Prefabs/UI/Popup/{name}");

        var go = Managers.Resource.Instantiate($"UI/Popup/{name}", parent ? parent : _root);
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = prefab.transform.position;

        return go.GetOrAddComponent<T>();
    }

    public bool RegisterPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        if (_popupObjects.ContainsKey(name))
        {
            Debug.Log($"[UIManager/RegisterPopupUI] The {name} already registered.");
            return false;
        }

        var popupUI = _root.GetComponentInChildren<T>();
        if (!popupUI)
        {
            popupUI = InstantiatePopupUI<T>(name);
        }

        _popupObjects.Add(name, popupUI);

        return true;
    }

    public bool UnregisterPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        if (!_popupObjects.ContainsKey(name))
        {
            Debug.Log($"[UIManager/UnregisterPopupUI] The {name} no registered.");
            return false;
        }

        Object.Destroy(_popupObjects[name].gameObject);
        _popupObjects.Remove(name);

        return true;
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        if (_popupObjects.TryGetValue(name, out var popupUI))
        {
            popupUI.gameObject.SetActive(true);
            return popupUI as T;
        }

        return null;
    }

    public void ClosePopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        if (_popupObjects.TryGetValue(name, out var popupUI))
        {
            popupUI.gameObject.SetActive(false);
        }
    }

    public void ClearPopupUI()
    {
        foreach (var popupUI in _popupObjects)
        {
            Object.Destroy(popupUI.Value.gameObject);
        }

        _popupObjects.Clear();
    }

    public void ClearAllPopupUI()
    {
        for (int i = 0; i < _root.childCount; i++)
        {
            Object.Destroy(_root.GetChild(i).gameObject);
        }

        _popupObjects.Clear();
    }

    public void Clear()
    {
        ClearSceneUI();
        ClearAllPopupUI();
    }
}
