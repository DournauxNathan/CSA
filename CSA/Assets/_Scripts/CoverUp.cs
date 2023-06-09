using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverUp : MonoBehaviour
{
    public bool isPlayerDetected = true;
    public Material aspect;

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerDetected = false;
            collision.GetComponent<PlayerController>().SetNewCamouflage(aspect.color);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerDetected = true;
            collision.GetComponent<PlayerController>().ResetCamouflage();
        }
    }
}
