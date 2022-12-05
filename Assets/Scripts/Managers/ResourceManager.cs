using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            var name = path;
            var index = path.LastIndexOf('/');
            if (index >= 0)
            {
                name = path.Substring(index + 1);
            }

            var original = Managers.Pool.GetOriginal(name);
            if (original)
            {
                return original as T;
            }
        }

        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        var prefab = Load<GameObject>($"Prefabs/{path}");
        if (!prefab)
        {
            Debug.Log($"[ResourceManager/Instantiate] Failed to load prefab. : {path}");
            return null;
        }

        if (prefab.TryGetComponent<Poolable>(out _))
        {
            return Managers.Pool.Pop(prefab, parent).gameObject;
        }

        var go = Object.Instantiate(prefab, parent);
        go.name = prefab.name;

        return go;
    }

    public void Destroy(GameObject go)
    {
        if (!go)
        {
            return;
        }

        if (go.TryGetComponent<Poolable>(out var poolable))
        {
            Managers.Pool.Push(poolable);
            return;
        }

        Object.Destroy(go);
    }
}
