using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [Header("")]
    [SerializeField] private PlayerAim crosshair;

    [SerializeField] private Transform hitCollider;

    private Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        hitCollider = other.transform;
        Debug.Log(other.name);

        crosshair.SubscribeTargetInfo(other.transform);
    }  


}
