using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [Range(1, 100), SerializeField] int poolsize;

    [field: SerializeField] public List<PooledObject> poolObjectList { get; private set; }


    [SerializeField] Vector2 offScreen;

    Dictionary<string, Stack<PooledObject>> poolObjectDictionaries;


    void Awake()
    {
        SetUp();
    }

    void SetUp()
    {
        if (poolObjectList.Count == 0 || poolObjectList == null)
        {
            return;
        }

        poolObjectDictionaries = new Dictionary<string, Stack<PooledObject>>();

        foreach (PooledObject poolObject in poolObjectList)
        {
            Stack<PooledObject> objectStack = new Stack<PooledObject>();
            for (int i = 0; i < poolsize; i++)
            {
                PooledObject instance = Instantiate(poolObject);
                instance._Instance = this;
                instance.name = poolObject.name;
                instance.transform.parent = this.transform;
                instance.gameObject.SetActive(false);
                objectStack.Push(instance);
            }
            poolObjectDictionaries.Add(poolObject.name, objectStack);
        }
    }


    public PooledObject GetObjectPooled(string name, Vector2 position)
    {
        if (string.IsNullOrEmpty(name) || !poolObjectDictionaries.ContainsKey(name))
        {
            return null;
        }


        if (poolObjectDictionaries[name].Count == 0)
        {
            PooledObject findedObject = poolObjectList.Find(poolObjectList => poolObjectList.name == name);
            PooledObject newInstance = Instantiate(findedObject, position, Quaternion.identity);
            newInstance._Instance = this;
            newInstance.name = findedObject.name;
            newInstance.transform.parent = this.transform;
            return newInstance;
        }

        PooledObject nextInstance = poolObjectDictionaries[name].Pop();
        nextInstance.transform.position = position;
        nextInstance.gameObject.SetActive(true);
        return nextInstance;
    }

    //int quantity
    public PooledObject GetObjectPooled(Vector2 position)
    {
        //if (poolObjectList.Count == 0) return;

        //for (int i = 0; i < quantity; i++)
        //{

        //}
        PooledObject nextInstance = null;
        foreach (var pooledObject in poolObjectList)
        {
            nextInstance = poolObjectDictionaries[pooledObject.name].Pop();
            nextInstance.transform.position = position;
            nextInstance.gameObject.SetActive(true);
        }
        return nextInstance;
    }

    public void ReturnToPool(PooledObject objectPool)
    {
        if (!poolObjectDictionaries.ContainsKey(objectPool.name))
        {
            Destroy(objectPool.gameObject);
            return;
        }
        else
        {
            objectPool.gameObject.SetActive(false);
            poolObjectDictionaries[objectPool.name].Push(objectPool);
        }
    }

}
