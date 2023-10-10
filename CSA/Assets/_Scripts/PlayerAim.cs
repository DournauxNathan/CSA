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
    private Vector3 interactorStartPos;

    public LayerMask _interactable;

    public float startOffset;
    public float endOffset;
    public float shootingSpeed;

    public bool isAimMode = false;
    public bool isTargetLocked = false;

    public bool isShooting = false;
    private bool GoBack;

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
        interactor.position = startPoint.position;

        interactorStartPos = interactor.position;
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
                
        if (target != null)
        {
            Shoot();
        }

        if (isAimMode && controller.Input.Shoot && !GoBack && !isShooting)
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

            if (!isShooting && !GoBack) // Position the Interactor
            {
                interactor.position = startPoint.position;
            }

            if (aim.magnitude > 0f && isTargetLocked) //When Lock on Interactable
            {
                endPoint.position = target.position;

                DrawAimLine(endPoint.position);
            }
            else if (aim.magnitude > 0f) //When aim without target
            {
                aim *= startOffset;
                startPoint.localPosition = aim2 ;

                aim *= endOffset;
                endPoint.localPosition= aim;

                DrawAimLine(endPoint.position);
                //crosshair.position = Camera.main.WorldToScreenPoint(endPoint.transform.position);
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
            isTargetLocked = false;

            isShooting = false;
            GoBack = false;
        }
    }

    private void Shoot()
    {
        if (isShooting && !GoBack)
        {
            interactor.position = Vector3.MoveTowards(interactor.position, endPoint.position, shootingSpeed * Time.deltaTime);            
            DetectAt(interactor.position, .25f, _interactable);
        }
        
        if (GoBack && !isShooting)
        {
           interactor.position = Vector3.MoveTowards(interactor.position, startPoint.position, shootingSpeed * Time.deltaTime);

            if (interactor.position == startPoint.position)
            {
                GoBack = false;
            }
        }
    }

    private bool DetectAt(Vector3 origin, float radius, LayerMask mask)
    {
        var hitCollider = Physics2D.OverlapCircle(origin, radius, _interactable);

        if (hitCollider != null)
        {
            GoBack = true;
            isShooting = false;

            CheckInteractable(hitCollider);
        }
        return hitCollider != null;
    }

    private bool CheckInteractable(Collider2D _col)
    {
        var interactable = _col.GetComponent<IInteractable>();

        if (interactable != null)
        {
            interactable.Interact();
            return true;
        }
        return false;
    }
    
    private bool DetectBetweenEndPoint(Vector3 origin, Vector3 direction)
    {
        if (Vector3.Distance(startPoint.position, endPoint.position) < maxRange)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxRange, _interactable);
            {
                Debug.DrawRay(origin, direction, Color.red);

                //If something was hit.
                if (hit.collider != null)
                {
                    LockTarget(hit.transform);
                    return isTargetLocked = true;
                }
            }
        }
        return isTargetLocked = false;
    }

    private void DrawAimLine(Vector3 endPos)
    {
        lr.enabled = true;

        interactor.gameObject.SetActive(true);
        crosshair.gameObject.SetActive(true);
              
        _startPos = startPoint.position;
        _startPos.z = 0;
        lr.SetPosition(0, _startPos);

        if (!isTargetLocked)
        {            
            _endPos = endPos;
            _endPos.z = 0;

            //change the length of the line renderer
            _endPos = _startPos + (CalculateDirection(_startPos, _endPos) * CalculateLineLength(_startPos, _endPos));
            lr.SetPosition(1, _endPos);

            DetectBetweenEndPoint(_startPos, (CalculateDirection(_startPos, _endPos) * CalculateLineLength(_startPos, _endPos)));

            /*//_crosshairPos = Camera.main.WorldToScreenPoint(_endPos);
            _crosshairPos.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            crosshair.position = _crosshairPos;  */        
        }
        else
        {
            _endPos = endPos;
            _endPos.z = 0;

            //change the length of the line renderer
            _endPos = _startPos + (CalculateDirection(_startPos, _endPos) * CalculateLineLength(_startPos, _endPos));
            lr.SetPosition(1, target.position);

            /*_crosshairPos = Camera.main.WorldToScreenPoint(target.position);
            _crosshairPos.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            crosshair.position = _crosshairPos;*/
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

    public void LockTarget(Transform _transform)
    {
        //Update the endPos
        target = _transform;
        //interactor.transform.position = startPoint.position;
    }

    public void SubscribeTargetInfo(Transform _target)
    {
        target = _target;
    }
}
