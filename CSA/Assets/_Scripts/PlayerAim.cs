using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private PlayerController controller;
    public bool isAimMode;

    public float offset;
    public Transform debugPoint;

    private Vector3 startPos;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Aim();        
    }

    private void Aim()
    {
        if (!controller.canMove)
        {
            float horizontalAxis = Input.GetAxis("Horizontal");
            float verticalAxis = Input.GetAxis("Vertical");
            verticalAxis = Mathf.Clamp(verticalAxis, 0f, 1f);
            Vector3 aim = new Vector3(horizontalAxis, verticalAxis,0).normalized;

            if (aim.magnitude > 0f)
            {
                aim *= offset;
                debugPoint.transform.localPosition = aim;
            }

            /*if (angle.z > 90 && angle.z < 200 )
            {
                transform.rotation = Quaternion.Euler(new Vector3(0,0, 89));
                //Rotate Player
            }

            if (angle.z < 270 && angle.z > 200)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 271));
                //Rotate Player
            }*/
        }
        else
        {
            debugPoint.transform.localPosition = startPos;
        }

    }

}
