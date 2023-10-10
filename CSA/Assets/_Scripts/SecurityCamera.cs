using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : Interactable, IInteractable
{
    [SerializeField] private Animator _anim;

    [Header("Field Of View Parameters")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform prefabFov;
    [SerializeField] private Transform endPoint;
    private FieldOfView fieldOfView;
    [Space(5)]
    [SerializeField] [Range(0f, 360f)] private float fov;
    [SerializeField] private float viewDistance;
    
    Vector3 aimDir;

    private void Start()
    {
        Reset();

        if (player == null) player = GameObject.Find("Player").GetComponent<PlayerController>();
        
        fieldOfView = Instantiate(prefabFov, null).GetComponent<FieldOfView>();
        fieldOfView.SetFoV(fov);
        fieldOfView.SetViewDistance(viewDistance);
    }

    private void Update()
    {
        Vector3 targetPosition = endPoint.position;
        aimDir = (targetPosition - transform.position).normalized;

        if (fieldOfView != null)
        {
            fieldOfView.SetOrigin(transform.position);
            fieldOfView.SetAimDirection(aimDir);
        }

        if (fieldOfView.enabled == true)
        {
            FindTargetPlayer();
        }
    }

    private void FindTargetPlayer()
    {        
        if (Vector3.Distance(GetPosition(), player.GetPosition()) < viewDistance)
        {
            //Player inside viewDistance
            Vector3 dirToPlayer = (player.GetPosition() - GetPosition()).normalized;

            //Player inside Field Of View
            if (Vector3.Angle(aimDir, dirToPlayer) < fov / 2f)
            {
                RaycastHit2D raycastHit2D = Physics2D.Raycast(GetPosition(), dirToPlayer, viewDistance, layerMask);

                if (raycastHit2D.collider != null && raycastHit2D.collider.enabled == true)
                {
                    Debug.Log(raycastHit2D.collider.gameObject);

                    if (raycastHit2D.collider.gameObject.GetComponent<PlayerController>() != null)
                    {
                        Debug.DrawLine(GetPosition(), player.GetPosition());
                        //Hit Player
                        GameManager.Instance.isPlayerDetected = true;
                        Alert();
                    }
                }
                else
                {
                    //Hit something else
                    GameManager.Instance.isPlayerDetected = false;
                }
            }
        }
    }

    public void Alert()
    {
        GameManager.Instance.RestartLevel();
    }

    #region Interactable interface
    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        Toogle();
        Debug.Log("Hey");
    }

    public void Timer()
    {
        throw new System.NotImplementedException();
    }

    public void Toogle()
    {
        if (isActive)
        {
            isActive = false;
            _anim.enabled = false;
            fieldOfView.enabled = false;

            StartCoroutine(DecreaseTimer());
            onDeactivate?.Invoke();
        }
        else if (!isActive)
        {
            isActive = true;
            _anim.enabled = true;
            fieldOfView.enabled = true;

            onActivate?.Invoke();
            coolDown = resetTimer;
        }

    }

    #endregion

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, endPoint.position);
    }
}
