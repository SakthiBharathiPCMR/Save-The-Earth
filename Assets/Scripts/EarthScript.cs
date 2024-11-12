using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EarthScript : MonoBehaviour
{

    private const float rotateDegree = -360f;

    public float rotateTime = 5f;
    public float scaleTime = 2f;
    public float scaleAmount = 0.8f;

    private int health = 10;
    private int currentHealth ;

    public AudioSource audioSource;

    public Image healthUI;
    public Transform healthBar;
    // Use this for initialization
    public void StartGame()
    {
        ToggleHealthBar(true);
        currentHealth = health;
        HealthUI();

        transform.DORotate(Vector3.forward * rotateDegree, rotateTime, RotateMode.FastBeyond360)
        .SetLoops(-1, LoopType.Restart)
        .SetEase(Ease.Linear);

       /* transform.DOScale((Vector2.one * scaleAmount), scaleTime)
        .SetLoops(-1, LoopType.Yoyo)
        .SetEase(Ease.Linear);*/

    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        Damage();
        audioSource.Play();

        if (currentHealth<=0)
        {
            /*Debug.Log("GameOver");*/

            GameManager.Instance.GameOver();


        }
    }

  

    private void Damage()
    {
        currentHealth--;
        HealthUI();
    }


    public void ToggleAudio(bool isActive)
    {
        audioSource.enabled = isActive;
    }



    private void HealthUI()
    {
        healthUI.fillAmount = (float)currentHealth / health;
    }

    private void ToggleHealthBar(bool isActive)
    {
        if(healthBar!=null)
        healthBar.gameObject.SetActive(isActive);
    }

    public void OnDisable()
    {
        ToggleHealthBar(false);
    }


}
