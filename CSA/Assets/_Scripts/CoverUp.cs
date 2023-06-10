using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverUp : MonoBehaviour
{
    public Material aspect;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.enabled = false;
            Debug.Log(collision.enabled);

            collision.GetComponent<PlayerController>().SetNewCamouflage(aspect.color);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Exit");
            collision.GetComponent<BoxCollider2D>().enabled = true;
            collision.GetComponent<PlayerController>().ResetCamouflage();
        }
    }
}
