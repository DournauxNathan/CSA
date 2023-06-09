using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : Detection
{
    [Header("")]
    [SerializeField] private PlayerAim crosshair;

   
    // Update is called once per frame
    void Update()
    {
        if (crosshair.isAimMode)
        {
            Detect();   
        }
    }

    public override void Detect()
    {

        Debug.Log(GetHitInfo().name);
        crosshair.SubscribeTargetInfo(GetHitInfo().transform);
    }  


}
