using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager<T> where T : MonoBehaviour
{

    private int poolQuantity;
    private List<T> availableObjects;
    private GameObject prefabToInstantiate;
    public int PoolQuantity
    {
        set { poolQuantity = value; }
    }

    public void InitializePool(GameObject prefabToInstantiate)
    {
        this.prefabToInstantiate = prefabToInstantiate;
        availableObjects = new List<T>(poolQuantity);
        for (int i = 0; i < poolQuantity; i++)
        {
            GameObject newObject = GameObject.Instantiate(prefabToInstantiate);
            newObject.SetActive(false);
            availableObjects.Add(newObject.GetComponent<T>());
        }
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        availableObjects.Add(obj);
    }

    public T InstantiateObject()
    {
        T objectToReturn = null;
        if (availableObjects.Count > 0)
        {
            objectToReturn = availableObjects[0];
            availableObjects.RemoveAt(0);
        }
        else
        {
            objectToReturn = GameObject.Instantiate(prefabToInstantiate).GetComponent<T>();
        }

        return objectToReturn;
    }


}