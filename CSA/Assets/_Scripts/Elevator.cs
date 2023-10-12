using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elevator : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _points;

    public UnityEvent onStart, onCall, onDelayed;
    bool doOnce = false;
    
    private bool _isCalledToPanel = false;

    private void Start()
    {
        onStart?.Invoke();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var step = _speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _isCalledToPanel ? _points[1].position : _points[0].position, step);

        if (transform.position == _points[1].position && !doOnce) 
        {
            doOnce = true;
            onDelayed?.Invoke();
        }
    }

    public void CallElevator()
    {
        onCall?.Invoke();
        _isCalledToPanel = !_isCalledToPanel;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = this.transform;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }

    void OnDrawGizmosSelected()
    {
        for (int i = 1; i < _points.Length; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(_points[i - 1].position, _points[i].position);
        }
    }

}
