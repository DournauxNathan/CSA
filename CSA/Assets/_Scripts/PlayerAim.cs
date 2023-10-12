using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform crosshair;

    private PlayerController controller;
    private float time;
    private Vector3 _crosshairPos;
    private Vector3 startPos;
    private Vector3 _startPos;
    private Vector3 _endPos;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        crosshair.gameObject.SetActive(false);
        startPos = transform.position;

        interactor.gameObject.SetActive(false);
        interactor.position = startPoint.position;

        interactorStartPos = interactor.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAimMode && controller.Input.Shoot)
        {
            isShooting = true;
            StartCoroutine(Shoot());
        }
        
        ToggleAimMode();
    }

    #region Aim
    [Header("AIM")]
    [SerializeField] private float endOffset;
    [SerializeField] private bool isAimMode = false;
    [SerializeField] private bool isTargetLocked = false;
    private void ToggleAimMode()
    {
        //If Player can't move bc its oress Aim button
        if (!controller.canMove)
        {
            EnterAimMode();
        }
        else
        {
            ExitAimMode();            
        }
    }

    private void EnterAimMode()
    {
        isAimMode = true;
        crosshair.gameObject.SetActive(true);

        if (!isShooting)
        {
            //Joystick Handle Movement
            HandleJoystickMovement();
        }

        //Draw Aim line Between Start & End Point
        DrawAimLine(crosshair.position);
    }

    private void ExitAimMode()
    {
        isAimMode = false;

        crosshair.gameObject.SetActive(false);
        interactor.gameObject.SetActive(false);

        lineRenderer.enabled = false;
        target = null;

        isTargetLocked = false;

        isShooting = false;
    }

    private void HandleJoystickMovement()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        //Clamp the verticalAxis between 0 and 1
        verticalAxis = Mathf.Clamp(verticalAxis, 0f, 1f);
        Vector3 aim = new Vector3(horizontalAxis, verticalAxis, 0).normalized;

        if (!isShooting) // Position the Interactor
        {
            interactor.position = startPoint.position;
        }
                
        if (aim.magnitude > 0f && !isTargetLocked)
        {
            startPoint.localPosition = aim;

            aim *= endOffset;
            crosshair.localPosition = aim;
        }
    }
    #endregion

    #region Shoot
    [Header("SHOOT")]
    [SerializeField] private float shootingSpeed;
    [SerializeField] private bool isShooting = false;

    IEnumerator Shoot()
    {
        Vector3 startPosition = interactor.position;
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(interactor.position, crosshair.position);
        float distanceCovered = 0f;

        while (distanceCovered < journeyLength)
        {
            float distance = (Time.time - startTime) * shootingSpeed;
            float journeyFraction = distance / journeyLength;

            interactor.position = Vector3.MoveTowards(startPosition, crosshair.position, journeyFraction);
            
            distanceCovered = Vector3.Distance(startPosition, interactor.position);
                        
            yield return null;            
        }

        DetectAt(interactor.position, radius, _interactable);
        StartCoroutine(BackTo());
    }

    IEnumerator BackTo()
    {
        Vector3 startPosition = interactor.position;
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(interactor.position, startPoint.position);
        float distanceCovered = 0f;

        while (distanceCovered < journeyLength)
        {
            float distance = (Time.time - startTime) * shootingSpeed;
            float journeyFraction = distance / journeyLength;

            interactor.position = Vector3.MoveTowards(startPosition, startPoint.position, journeyFraction);

            distanceCovered = Vector3.Distance(startPosition, interactor.position);

            yield return null;
        }

        isShooting = false;
        controller.canMove = true;
        controller.SetClick(0);
    }

    #endregion

    #region Detection & Collision
    [Header("DETECTION & COLLISION")]
    [SerializeField] private Transform interactor;
    [SerializeField] private float radius;
    [SerializeField] private float maxRange;
    [Space(5)]
    [SerializeField] private LayerMask _interactable;
    [SerializeField] private Transform target;

    private Vector3 interactorStartPos;

    private bool DetectAt(Vector3 origin, float radius, LayerMask mask)
    {
        var hitCollider = Physics2D.OverlapCircle(origin, radius, _interactable);

        if (hitCollider != null)
        {
            CheckInteractable(hitCollider);
            isShooting = false;
        }
        return hitCollider != null;
    }

    private void OnDrawGizmos()
    {
        //Debug DetectAt method
        Gizmos.DrawSphere(interactor.position, radius);
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
    
    private bool DetectBetweenCrosshair(Vector3 origin, Vector3 direction)
    {
        if (Vector3.Distance(startPoint.position, crosshair.position) < maxRange)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxRange, _interactable);
            {
                Debug.DrawRay(origin, direction, Color.red, .1f);

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
    #endregion

    #region Crosshair & Line Feedback
    private void DrawAimLine(Vector3 endPos)
    {
        lineRenderer.enabled = true;

        interactor.gameObject.SetActive(true);
              
        _startPos = startPoint.position;
        _startPos.z = 0;
        lineRenderer.SetPosition(0, _startPos);

        if (!isTargetLocked)
        {            
            _endPos = endPos;
            _endPos.z = 0;

            //change the length of the line renderer
            _endPos = _startPos + (CalculateDirection(_startPos, _endPos) * CalculateLineLength(_startPos, _endPos));
            lineRenderer.SetPosition(1, _endPos);

            DetectBetweenCrosshair(_startPos, (CalculateDirection(_startPos, _endPos) * CalculateLineLength(_startPos, _endPos)));
        }
        else
        {
            _endPos = endPos;
            _endPos.z = 0;

            //change the length of the line renderer
            _endPos = _startPos + (CalculateDirection(_startPos, _endPos) * CalculateLineLength(_startPos, _endPos));
            lineRenderer.SetPosition(1, target.position);

            MoveCrosshair(target.position);
        }
    }

    private void MoveCrosshair(Vector3 position)
    {
        crosshair.position = position;
    }
    #endregion
    
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
