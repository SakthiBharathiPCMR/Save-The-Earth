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
    private Vector3 startScale;

    private Transform healthBar;



    public int health = 3;
    public Image healthUI;

    private bool isAlive;
    private bool isClickable;

    public ParticleSystem explosionEffect;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        healthBar = transform.GetChild(0).transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void ToogleComponent(bool isActive)
    {
        healthBar.gameObject.SetActive(isActive);
        spriteRenderer.enabled = isActive;
    }

    public void StartAstroid()
    {
        startScale = transform.localScale;
        currentHealth = health;
        HealthUI();
        isAlive = true;
        isClickable = true;

        float index = Random.Range(minTime, maxTime);
        StartCoroutine(MoveToEarth(index));
    }



    private void OnMouseDown()
    {
        if (!isAlive || !isClickable) return;
        damegeHealth();
        GameManager.Instance.PlayInDamage();
        isClickable = false;
        StartCoroutine(BlockInput());
        if (currentHealth <= 0)
        {
            StopAllCoroutines();
            isAlive = false;
            GameManager.Instance.UpdateScore();
            Invoke("DelayTurnOff", .2f);
        }
        Shake();


    }

    private IEnumerator BlockInput()
    {
        yield return new WaitForSeconds(.2f);
        isClickable = true;
    }



    private void DelayTurnOff()
    {
        explosionEffect.Play();
        ToogleComponent(false);
        StartCoroutine(DelayAstroidTurnOff());
        GameManager.Instance.PlayInExplosion();
        transform.localScale = startScale;
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
            if (!GameManager.Instance.isGameActive)
            {
                StopAllCoroutines();
                gameObject.SetActive(false);

            }
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null;

        }

        transform.position = endPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAlive) return;
        ToogleComponent(false);
        GameManager.Instance.PlayInExplosion();
        explosionEffect.Play();
        StartCoroutine(DelayAstroidTurnOff());
    }


    private IEnumerator DelayAstroidTurnOff()
    {
        yield return new WaitForSeconds(1f);
        ToogleComponent(true);
        gameObject.SetActive(false);

    }



}
