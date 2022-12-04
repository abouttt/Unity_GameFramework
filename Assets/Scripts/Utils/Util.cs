using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static GameObject FindChild(GameObject go, string name = null, bool bRecursive = false)
    {
        var transform = FindChild<Transform>(go, name, bRecursive);
        if (!transform)
        {
            return null;
        }

        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool bRecursive = false) where T : UnityEngine.Object
    {
        if (!go)
        {
            return null;
        }

        if (bRecursive)
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name.Equals(name))
                {
                    return component;
                }
            }
        }
        else
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                var transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name.Equals(name))
                {
                    var component = transform.GetComponent<T>();
                    if (component)
                    {
                        return component;
                    }
                }
            }
        }

        return null;
    }
}
