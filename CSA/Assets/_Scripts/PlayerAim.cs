using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private PlayerController controller;
    public Transform crosshair;
    [SerializeField] private LineRenderer lr;
    public Transform startPoint;
    public Transform endPoint;
    public Transform interactor;

    public float startOffset;
    public float endOffset;
    public float shootingSpeed;

    public bool isAimMode = false;
    public bool isShooting = false;
    public bool isObjectBetweenPoints = false;

    private float time;
    private Vector3 _crosshairPos;
    private Vector3 startPos;
    private Vector3 _startPos;
    private Vector3 _endPos;

    public Transform target;
    [SerializeField] private float maxRange;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        crosshair.gameObject.SetActive(false);
        endPoint.gameObject.SetActive(false);
        startPos = transform.position;

        interactor.gameObject.SetActive(false);
        interactor.transform.position = startPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        Aim();

        if (target != null)
        {
            Shoot();
        }

        if (isAimMode && controller.Input.Shoot)
        {
            isShooting = true;
        }
    }


    private void Aim()
    {
        if (!controller.canMove)
        {
            isAimMode = true;
            endPoint.gameObject.SetActive(true);

            float horizontalAxis = Input.GetAxis("Horizontal");
            float verticalAxis = Input.GetAxis("Vertical");
            verticalAxis = Mathf.Clamp(verticalAxis, 0f, 1f);
            Vector3 aim = new Vector3(horizontalAxis, verticalAxis,0).normalized;
            Vector3 aim2 = new Vector3(horizontalAxis, verticalAxis, 0).normalized;

            if (!isShooting)
            {
                interactor.transform.position = startPoint.position;
            }

            if (aim.magnitude > 0f && isObjectBetweenPoints)
            {
                endPoint.transform.position = target.position;

                DrawAimLine(endPoint.position);
            }
            else if (aim.magnitude > 0f)
            {
                aim *= startOffset;
                startPoint.transform.localPosition = aim2 ;

                aim *= endOffset;
                endPoint.transform.localPosition = aim;

                DrawAimLine(endPoint.position);
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
            crosshair.gameObject.SetActive(false);
            interactor.gameObject.SetActive(false);
            endPoint.gameObject.SetActive(false);
            
            lr.enabled = false;
            target = null;
            
            isAimMode = false;
            isObjectBetweenPoints = false;
        }
    }

    private void Shoot()
    {
        if (isShooting)
        {
            interactor.position = Vector3.MoveTowards(interactor.position, target.position, shootingSpeed * Time.deltaTime);
            Debug.Log("Shoot");

        }
    }


    private bool DetectBetweenEndPoint(Vector3 origin, Vector3 direction)
    {
        if (Vector3.Distance(startPoint.position, endPoint.position) < maxRange)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxRange, controller._interactable);
            {
                Debug.DrawRay(origin, direction, Color.red);

                //If something was hit.
                if (hit.collider != null)
                {
                    target = hit.transform;
                    interactor.transform.position = startPoint.position;
                    return isObjectBetweenPoints = true;
                }
            }
        }
        return isObjectBetweenPoints = false;
    }

    private void DrawAimLine(Vector3 endPos)
    {
        lr.enabled = true;

        interactor.gameObject.SetActive(true);
        crosshair.gameObject.SetActive(true);
              
        _startPos = startPoint.position;
        _startPos.z = 0;
        lr.SetPosition(0, _startPos);

        if (!isObjectBetweenPoints)
        {            
            _endPos = endPos;
            _endPos.z = 0;

            //change the length of the line renderer
            _endPos = _startPos + (CalculateDirection(_startPos, _endPos) * CalculateLineLength(_startPos, _endPos));
            lr.SetPosition(1, _endPos);

            DetectBetweenEndPoint(_startPos, (CalculateDirection(_startPos, _endPos) * CalculateLineLength(_startPos, _endPos)));

            _crosshairPos = Camera.main.WorldToScreenPoint(_endPos);
            _crosshairPos.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            crosshair.position = _crosshairPos;          
        }
        else
        {
            _endPos = endPos;
            _endPos.z = 0;

            //change the length of the line renderer
            _endPos = _startPos + (CalculateDirection(_startPos, _endPos) * CalculateLineLength(_startPos, _endPos));
            lr.SetPosition(1, target.position);

            _crosshairPos = Camera.main.WorldToScreenPoint(target.position);
            _crosshairPos.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            crosshair.position = _crosshairPos;
        }        
    }

    public Vector3 CalculateDirection(Vector3 _start, Vector3 _end)
    {
        Vector3 _direction = _end - _start;
        _direction.z = 0;
        _direction.Normalize();
        return _direction;
    }

    public float CalculateLineLength(Vector3 _startPos, Vector3 _endPos)
    {
        return Mathf.Clamp(Vector2.Distance(_startPos, _endPos), 0, Vector2.Distance(_startPos, _endPos));
    }

    public void LockTarget()
    {
        //Update the endPos

    }

    public void SubscribeTargetInfo(Transform _target)
    {
        target = _target;
    }
}
