using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{


    private float spawnRate = 1.5f;
    private float radius = 12f;

    // Use this for initialization
    void Start()
    {


    }

    public void StartGame()
    {
        StartCoroutine(SpawnAstroids());
    }



    private IEnumerator SpawnAstroids()
    {
        while (true)
        {
            if (!GameManager.Instance.isGameActive)
            {
                StopAllCoroutines();
                
            }
                yield return new WaitForSeconds(spawnRate);

                GameObject bullet = ObjectPooling.Instance.GetPooledObject();
                if (bullet != null)
                {
                    Vector2 randomDirection = GetRandomPositionOnCircle(Vector2.one, radius);

                    bullet.transform.position = randomDirection;
                    bullet.transform.rotation = Quaternion.identity;
                    bullet.SetActive(true);
                    FollowEarth followEarth = bullet.GetComponent<FollowEarth>();
                    followEarth.StartAstroid();
                }
            
        }
    }

    private Vector2 GetRandomPositionOnCircle(Vector2 center, float radius)
    {
        // Generate a random angle between 0 and 360 degrees
        float randomAngle = UnityEngine.Random.Range(0f, 360f);

        // Convert the angle to radians
        float angleInRadians = randomAngle * Mathf.Deg2Rad;

        // Calculate the position on the circle using sine and cosine
        float x = center.x + radius * Mathf.Cos(angleInRadians);
        float y = center.y + radius * Mathf.Sin(angleInRadians);

        // Return the random position
        return new Vector2(x, y);
    }

}
