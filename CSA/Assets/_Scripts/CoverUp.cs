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

            collision.GetComponent<PlayerController>().SetNewCamouflage(aspect.color);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<BoxCollider2D>().enabled = true;
            collision.GetComponent<PlayerController>().ResetCamouflage();
        }
    }
}
