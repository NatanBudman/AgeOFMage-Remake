using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fairy : Enemy
{
    private Collider2D[] Colliders;
    public int[] AllyLayer;
    public float radius = 10;
    public float RangeRevive = 1.5f;
    public override void StartCall()
    {
        base.StartCall();
        Colliders = new Collider2D[15];
    }
    public override void Move()
    {
        base.Move();

        int count = Physics2D.OverlapCircleNonAlloc(transform.position, radius, Colliders);

        if (count < 0) return;

        Collider2D nearestCollider = null;
        float nearestDistance = Mathf.Infinity;

        for (int i = 0; i < count; i++)
        {
            Collider2D collider = Colliders[i];
            Enemy ally = collider.GetComponent<Enemy>();
            if (collider != null && collider != this)
            {
                if (CheckCollisionLayers(collider, AllyLayer) && ally.isDeath)
                {
                    float distance = Vector2.Distance(transform.position, collider.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestCollider = collider;
                        nearestDistance = distance;
                    }
                }
            }
        }
        if (nearestCollider != null )
        {
            Vector2 dir = nearestCollider.transform.position - transform.position;
            Movement(dir);

            float diffDistanmce = Vector2.Distance(transform.position, nearestCollider.transform.position);
            if (diffDistanmce < RangeRevive) 
            {
                HealthController health = nearestCollider.GetComponent<HealthController>();
                if (health != null)
                    health.OnReavive();

                Debug.Log(nearestCollider.name);
            }
        }
    }
    private void Movement(Vector2 direccion) 
    {
        Rotate(direccion);

        direccion.Normalize();
        animator.SetFloat("Vel", rb.velocity.magnitude);
        rb.velocity = direccion * Speed;
    }
}
