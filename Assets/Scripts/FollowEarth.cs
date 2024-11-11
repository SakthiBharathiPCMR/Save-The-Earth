using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class FollowEarth : MonoBehaviour
{
    private int currentHealth;
    private float minTime = 5f;
    private float maxTime = 10f;



    public int health = 3;
    public Image healthUI;


    // Use this for initialization
    void Start()
    {
        currentHealth = health;
        HealthUI();

        float index = Random.Range(minTime, maxTime);
        StartCoroutine(MoveToEarth(index));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        damegeHealth();
        if (currentHealth <= 0)
        {
            StopAllCoroutines();
            Invoke("DelayTurnOff", .2f);
        }
        Shake();


    }

    private void DelayTurnOff()
    {
        gameObject.SetActive(false);
    }

    private void Shake()
    {
        transform.DOShakeScale(0.2f, strength: 0.5f, vibrato: 15, randomness: 80, fadeOut: true);
    }

    private void damegeHealth()
    {
        currentHealth--;
        HealthUI();
    }

    private void HealthUI()
    {
        healthUI.fillAmount = (float)currentHealth / health;
    }

    private IEnumerator MoveToEarth(float totalTime)
    {
        float elapsedTime = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = Vector3.zero;

        while (elapsedTime < totalTime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.SetActive(false);
    }



}
