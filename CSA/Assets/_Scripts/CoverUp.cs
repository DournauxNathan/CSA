using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverUp : MonoBehaviour
{
    public Material aspect;
    public bool isEnter;
    private PlayerController player;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isEnter = true;
            player = collision.GetComponent<PlayerController>();
            player.SetNewCamouflage(aspect.color);
        }
    }

    private void FixedUpdate()
    {
        if (isEnter && Vector2.Distance(player.transform.position, this.transform.position) > 2f)
        {
            isEnter = false;
            player.ResetCamouflage();
        }
    }
}
