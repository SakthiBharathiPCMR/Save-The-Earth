using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEarth : MonoBehaviour
{
    private int currentHealth;

    public int health = 3;
    // Use this for initialization
    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        damegeHealth();
        if(currentHealth<=0)
        {
            Destroy(gameObject);
        }
            
    }

    private void damegeHealth()
    {
        currentHealth--;
    }


}
