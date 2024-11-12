using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Transition : MonoBehaviour
{


    public Transform endTransform;
    private Vector3 startPos;
    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
    }



    public void StartTransition()
    {
        transform.DOMove(endTransform.position, 2f, false);
        StartCoroutine(ResetPosition());
    }


    private IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(3f);
        transform.position = startPos;
    }
}
