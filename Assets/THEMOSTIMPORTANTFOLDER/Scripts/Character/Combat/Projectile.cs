using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Projectile : MonoBehaviour
{
    [SerializeField] private LayerMask EnemyLayerMask;
    private float MaxRadius = 10f;
    private float radius = 0f;
    private Rigidbody2D rb;
    private float DebuffDuration = 1.5f;
    private float AnimTime = 1f;
    private void Update()
    {
        Shader.SetGlobalVector("_CenterPoint", new Vector4(transform.position.x, transform.position.y, transform.position.z, radius));
        // Debug.Log(radius);
    }

    public void Shoot(Vector3 dir)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = dir * 10f;
        rb.gravityScale = 0f;
    }

    private void OnDestroy()
    {
        Shader.SetGlobalVector("_CenterPoint", Vector4.zero);
    }

    void DisableProjectile()
    {
        Shader.SetGlobalVector("_CircleColor", new Vector4(0.3f, 0, .5f,0.5f));
        Shader.SetGlobalVector("_EnemyColor", new Vector4(0.1f, 0.1f, .1f,0.5f));
        rb.velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
    }

    void CastImpotence()
    {
        // List<RaycastHit2D> results = new List<RaycastHit2D>();
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.layerMask = EnemyLayerMask;
        contactFilter2D.useLayerMask = true;
        contactFilter2D.useTriggers = true;
        Physics2D.OverlapCircle(transform.position, MaxRadius, contactFilter2D, results);
        foreach (var collider2D in results)
        {
            Debug.Log(collider2D.transform.name);
            collider2D.transform.GetComponent<Bubblable>().Bubbled(DebuffDuration + AnimTime);
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        DOTween.To(() => radius, x => radius = x, MaxRadius, DebuffDuration).onComplete = () =>
        {
            Invoke("DestroySelf", AnimTime);
        };
        Debug.Log(other.gameObject.layer + " : " + other.gameObject.name);
        CastImpotence();
        DisableProjectile();
    }
}
