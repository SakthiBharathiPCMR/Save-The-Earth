using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{


    public float spawnRate = 2f;
    // Use this for initialization
    void Start()
    {

        
            int index = Random.Range(0, 5);

            StartCoroutine(SpawnAstroids(index));

    }


    private IEnumerator SpawnAstroids(int index)
    {
        yield return new WaitForSeconds(index);

        GameObject bullet = ObjectPooling.Instance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = Vector2.right * 5;
            bullet.transform.rotation = Quaternion.identity;
            bullet.SetActive(true);
        }
    }
}
