using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private static ObjectPool instance;
    private GameObject pool;
    public Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();

    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ObjectPool();
            }
            return instance;
        }
    }

    public GameObject GetObject(GameObject prefab)
    {
        GameObject obj;
        if (!objectPool.ContainsKey(prefab.name) || objectPool[prefab.name].Count == 0)
        {
            obj = Object.Instantiate(prefab);
            PushObject(obj);
            if (pool == null)
                pool = new GameObject("ObjectPool");
            GameObject child = GameObject.Find(prefab.name);
            if (!child)
            {
                child = new GameObject(prefab.name);
                child.transform.SetParent(pool.transform);
            }
            obj.transform.SetParent(child.transform);
        }
        obj = objectPool[prefab.name].Dequeue();
        obj.SetActive(true);
        return obj;

    }

    public GameObject GetObjectButNotActive(GameObject prefab)
    {
        GameObject obj = null;
        if (prefab != null)
            if (!objectPool.ContainsKey(prefab.name) || objectPool[prefab.name].Count == 0)
            {

                obj = Object.Instantiate(prefab);
                PushObject(obj);
                if (pool == null)
                    pool = new GameObject("ObjectPool");
                GameObject child = GameObject.Find(prefab.name);
                if (!child)
                {
                    child = new GameObject(prefab.name);
                    child.transform.SetParent(pool.transform);
                }
                obj.transform.SetParent(child.transform);
            }
        obj = objectPool[prefab.name].Dequeue();
        obj.SetActive(false);
        return obj;
    }

    public void PushObject(GameObject prefab)
    {
        string _name = prefab.name.Replace("(Clone)", string.Empty);
        if (!objectPool.ContainsKey(_name))
            objectPool.Add(_name, new Queue<GameObject>());
        objectPool[_name].Enqueue(prefab);
        prefab.SetActive(false);
    }

}
