using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    [Header("Patrol Parameters")]
    public List<Transform> waypoints;
    public float speed;
    public float enemyWaitTime;
    public bool isMoving = true;
    public int destinationPoint = 0;
    
    [Header("Weakspot")]
    public List<GameButton> weakspots;
    public int weakspotsCount;

    [Header("Field Of View Parameters")]
    [SerializeField] private Transform prefabFov;
    private FieldOfView fieldOfView;
    [Space(5)]
    [SerializeField] [Range(0f, 360f)] private float fov;
    [SerializeField] private float viewDistance;

    private SpriteRenderer sprite;
    private Transform target;
    private PlayerController player;
    Vector3 aimDir;

    [Space(10)]
    public UnityEvent onDestroy;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null) player = GameObject.Find("Player").GetComponent<PlayerController>();

        sprite = GetComponent<SpriteRenderer>();

        fieldOfView = Instantiate(prefabFov, null).GetComponent<FieldOfView>();
        fieldOfView.SetFoV(fov);
        fieldOfView.SetViewDistance(viewDistance);

        weakspotsCount = weakspots.Count;
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (target.position - this.transform.position).normalized;

        if (isMoving)
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }

        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destinationPoint = (destinationPoint + 1) % waypoints.Count;
            StartCoroutine(WaitTime());


            if (destinationPoint == 0)
            {
                sprite.flipX = !sprite.flipX;
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else if (destinationPoint > 0)
            {
                sprite.flipX = !sprite.flipX;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }

        aimDir = (target.position - transform.position).normalized;

        if (fieldOfView != null)
        {
            fieldOfView.SetOrigin(transform.position);
            fieldOfView.SetAimDirection(aimDir);
        }
    }

    private void LateUpdate()
    {
        /*if (fov.IsPlayerDetected())
        {
            GameManager.Instance.RestartLevel();
        }*/
    }

    public void UpdateWeakSpotCount()
    {
        weakspotsCount--;

        if (weakspotsCount <= 0)
        {
            onDestroy?.Invoke();
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator WaitTime()
    {
        isMoving = false;
        yield return new WaitForSeconds(enemyWaitTime);

        target = waypoints[destinationPoint];
        isMoving = true;
    }
}
