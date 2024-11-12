using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Transition : MonoBehaviour
{


    public Transform endTransform;
    public Transform startTransform;
 


    public void StartTransition()
    {
        transform.DOMove(endTransform.position, 2f, false);
        StartCoroutine(ResetPosition());
    }


    private IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(2f);
        transform.position = startTransform.position;
    }
}
