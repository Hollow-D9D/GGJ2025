using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private GameObject Projectile;

    private bool CanUseBubble = true;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1) && CanUseBubble)
        {
            Shoot();
            
            CanUseBubble = false;
        }
    }
    
    void Reload() => CanUseBubble = true;
    
    public void Shoot()
    {
        Vector3 mouseWorldPos = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.nearClipPlane));
        Debug.Log(mouseWorldPos);
        mouseWorldPos.z = transform.position.z;
        Vector3 dir = (mouseWorldPos - transform.position).normalized;
        Instantiate(Projectile, transform.position, Quaternion.identity).GetComponent<Projectile>().Shoot(dir);
        Invoke("Reload", 4f);
    }
}
