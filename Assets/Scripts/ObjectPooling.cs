using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{

    public static ObjectPooling Instance;
    public List<GameObject> pooledObjects;
    public GameObject[] objectsToPool;
    public int amountToPool;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

        }
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < amountToPool; i++)
        {

            foreach (GameObject gameObject in objectsToPool)
            {
                tmp = Instantiate(gameObject, transform);
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }

        }
    }


    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            int index = Random.Range(0, pooledObjects.Count);
            if (!pooledObjects[index].activeInHierarchy)
            {
                return pooledObjects[index];
            }
        }
        return null;
    }
}
