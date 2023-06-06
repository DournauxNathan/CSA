using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private PlayerController controller;
    public bool isAimMode = false;
    public bool isObjectBetweenPoints = false;

    public Transform startPoint;
    public float startOffset;
    public Transform endPoint;
    public float endOffset;
    public Transform crosshair;
    private Vector3 _crosshairPos;

    private Vector3 startPos;
    [SerializeField] private LineRenderer lr;
    private Vector3 _startPos;
    private Vector3 _endPos;
    private Vector3 _direction;

    private Transform target;
    [SerializeField] private float maxRange;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        crosshair.gameObject.SetActive(false);
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
            isAimMode = true;
            float horizontalAxis = Input.GetAxis("Horizontal");
            float verticalAxis = Input.GetAxis("Vertical");
            verticalAxis = Mathf.Clamp(verticalAxis, 0f, 1f);
            Vector3 aim = new Vector3(horizontalAxis, verticalAxis,0).normalized;
            Vector3 aim2 = new Vector3(horizontalAxis, verticalAxis, 0).normalized;

            if (aim.magnitude > 0f && isObjectBetweenPoints)
            {
                endPoint.transform.localPosition = target.localPosition;

                DrawAimLine(endPoint.position);
            }
            else if (aim.magnitude > 0f)
            {
                aim *= startOffset;
                startPoint.transform.localPosition = aim2;

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
            isAimMode = false;
            endPoint.transform.localPosition = startPos;
            lr.enabled = false;
            isObjectBetweenPoints = false;
            crosshair.gameObject.SetActive(false);
            target = null;
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
                    return isObjectBetweenPoints = true;
                }
            }
        }
        return isObjectBetweenPoints = false;
    }

    private void DrawAimLine(Vector3 endPos)
    {
        lr.enabled = true;
        crosshair.gameObject.SetActive(true);

        _direction = endPoint.position - startPoint.position;
        _direction.z = 0;
        _direction.Normalize();

        _startPos = startPoint.position;
        _startPos.z = 0;
        lr.SetPosition(0, _startPos);

        if (!isObjectBetweenPoints)
        {            
            _endPos = endPos;
            _endPos.z = 0;

            //change the length of the line renderer
            float lineLength = Mathf.Clamp(Vector2.Distance(_startPos, _endPos), 0, Vector2.Distance(_startPos, _endPos));
            _endPos = _startPos + (_direction * lineLength);
            lr.SetPosition(1, _endPos);

            DetectBetweenEndPoint(_startPos, (_direction * lineLength));

            _crosshairPos = Camera.main.WorldToScreenPoint(_endPos);
            _crosshairPos.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            crosshair.position = _crosshairPos;
        }
        else
        {
            _endPos = endPos;
            _endPos.z = 0;

            //change the length of the line renderer
            float lineLength = Mathf.Clamp(Vector2.Distance(_startPos, _endPos), 0, Vector2.Distance(_startPos, _endPos));
            _endPos = _startPos + (_direction * lineLength);
            lr.SetPosition(1, target.localPosition);

            _crosshairPos = Camera.main.WorldToScreenPoint(target.localPosition);
            _crosshairPos.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            crosshair.position = _crosshairPos;
        }
        
    }

    public void LockTarget()
    {
        //Update the endPos

    }

    public void GetTargetInfo(Transform _target)
    {
        target = _target;
    }
}
