using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;

    Rigidbody2D _rb;
    float _dirHorizontal;
    float _dirVertical;

    private void Awake() => _rb = GetComponent<Rigidbody2D>();


    void Start() => _rb.gravityScale = 0;


    void FixedUpdate()
    {
        _dirHorizontal = Input.GetAxisRaw("Horizontal");
        _dirVertical = Input.GetAxisRaw("Vertical");
        
        _rb.velocity = new Vector2(_dirHorizontal, _dirVertical) * speed;
        if (_rb.velocity.magnitude > speed)
        {
            _rb.velocity = _rb.velocity.normalized * speed;
        }

        DetermineRotation();
        
        
    }

    private void DetermineRotation()
    {
        if (_dirHorizontal > 0)
        {
            if (_dirVertical > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 315);
            }
            else if (_dirVertical < 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 225);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 270);
            }
        }
        else if(_dirHorizontal < 0)
        {
            if (_dirVertical > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 45);
            }
            else if (_dirVertical < 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 135);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 90);
            }
        }
        else
        {
            if (_dirVertical > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (_dirVertical < 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 180);
            }
        }
    }
}
