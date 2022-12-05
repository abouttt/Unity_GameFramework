using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager
{
    #region Pool
    private class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; private set; }

        private Stack<Poolable> _poolStack = new();

        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject($"{Original.name}_Root").transform;

            for (int i = 0; i < count; i++)
            {
                Push(create());
            }
        }

        public void Push(Poolable poolable)
        {
            poolable.IsUsing = false;
            poolable.transform.SetParent(Root);
            poolable.gameObject.SetActive(false);

            _poolStack.Push(poolable);
        }

        public Poolable Pop(Transform parent)
        {
            var poolable = _poolStack.Count > 0 ? _poolStack.Pop() : create();
            poolable.gameObject.SetActive(true);
            poolable.transform.SetParent((parent == null) ? Root : parent);
            poolable.IsUsing = true;

            return poolable;
        }

        private Poolable create()
        {
            var go = Object.Instantiate(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }
    }
    #endregion

    private Dictionary<string, Pool> _pool = new();
    private Transform _root = null;

    public void Init()
    {
        var root = GameObject.Find("[Pool_Root]");
        if (!root)
        {
            root = new GameObject { name = "[Pool_Root]" };
        }

        _root = root.transform;
    }

    public void CreatePool(GameObject original, int count = 5)
    {
        var name = original.name;
        if (_pool.ContainsKey(name))
        {
            Debug.Log($"[PoolManager/CreatePool] The {name}_Pool already exist.");
            return;
        }

        var pool = new Pool();
        pool.Init(original, count);
        pool.Root.SetParent(_root);

        _pool.Add(name, pool);
    }

    public void Push(Poolable poolable)
    {
        if (!poolable)
        {
            Debug.Log($"[PoolManager/Push] The {poolable.name} is null.");
            return;
        }

        var name = poolable.name;
        if (!_pool.ContainsKey(name))
        {
            Debug.Log($"[PoolManager/Push] The {name}_Pool no exist.");
            Object.Destroy(poolable.gameObject);
            return;
        }

        _pool[name].Push(poolable);
    }

    public Poolable Pop(GameObject original, Transform parent = null)
    {
        var name = original.name;
        if (!_pool.ContainsKey(name))
        {
            CreatePool(original);
        }

        return _pool[name].Pop(parent);
    }

    public GameObject GetOriginal(string name)
    {
        if (!_pool.ContainsKey(name))
        {
            return null;
        }

        return _pool[name].Original;
    }

    public void Clear()
    {
        foreach (Transform child in _root)
        {
            Object.Destroy(child.gameObject);
        }

        _pool.Clear();
    }

    public void ClearPool(string name)
    {
        if (!_pool.ContainsKey(name))
        {
            Debug.Log($"[PoolManager/ClearPool] The {name}_Pool no exist.");
            return;
        }

        Object.Destroy(_pool[name].Root.gameObject);
        _pool.Remove(name);
    }
}
