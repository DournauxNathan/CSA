using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    public List<Transform> waypoints;
    public List<GameButton> weakspots;
    public int weakspotsCount;
    public SpriteRenderer sprite;
    public FieldOfView fov;

    public float speed;
    public float enemyWaitTime;
    public bool isMoving = true;

    private Transform target;
    public int destinationPoint = 0;

    public UnityEvent onDestroy;

    // Start is called before the first frame update
    void Start()
    {
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
            target = waypoints[destinationPoint];


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
        isMoving = true;
    }
}
