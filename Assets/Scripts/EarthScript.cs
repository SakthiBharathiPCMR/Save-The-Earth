using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EarthScript : MonoBehaviour
{

    private const float rotateDegree = -360f;

    public float rotateTime = 5f;
    public float scaleTime = 2f;
    public float scaleAmount = 0.8f;

    // Use this for initialization
    void Start()
    {
        transform.DORotate(Vector3.forward * rotateDegree, rotateTime, RotateMode.FastBeyond360)
        .SetLoops(-1, LoopType.Restart)
        .SetEase(Ease.Linear);

        transform.DOScale((Vector2.one * scaleAmount), scaleTime)
        .SetLoops(-1, LoopType.Yoyo)
        .SetEase(Ease.Linear);

    }

 


}
