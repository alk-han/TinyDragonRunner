using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    [SerializeField] private float rotateSpeed;


    private void Start()
    {
        transform.DORotate(new Vector3(0, 360f, 0 ), 5.0f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetRelative()
            .SetEase(Ease.Linear);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.DOKill();
            Destroy(gameObject);
        }
    }


    private void OnDestroy()
    {
        transform.DOKill();
    }
}
