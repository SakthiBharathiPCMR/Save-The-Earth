using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class FollowEarth : MonoBehaviour
{
    private int currentHealth;
    private float minTime = 8f;
    private float maxTime = 10f;



    public int health = 3;
    public Image healthUI;

    private bool isAlive;


    public void StartAstroid()
    {
        currentHealth = health;
        HealthUI();
        isAlive = true;

        float index = Random.Range(minTime, maxTime);
        StartCoroutine(MoveToEarth(index));
    }



    private void OnMouseDown()
    {
        if (!isAlive) return;
        damegeHealth();
        if (currentHealth <= 0)
        {
            StopAllCoroutines();
            isAlive = false;
            GameManager.Instance.UpdateScore();
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
            if (GameManager.Instance.isGameActive)
            {
                transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / totalTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        transform.position = endPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.SetActive(false);
    }



}
