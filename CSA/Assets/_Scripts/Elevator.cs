using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _points;

    private bool _isCalledToPanel = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var step = _speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _isCalledToPanel ? _points[1].position : _points[0].position, step);
    }

    public void CallElevator()
    {
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
