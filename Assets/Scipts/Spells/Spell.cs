using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour, SpellInterface
{
    [HideInInspector]public PoolSpell SpellPool;
    public Rigidbody2D rb;
    public float Speed;
    public int Damage;
    public float Cost;

    public int[] Layers;
    public int[] LayersIgnore;

    private void Start()
    {
        StartCall();
    }
    public virtual void StartCall()
    {

    }
    public virtual void DisableSpell(GameObject Spell)
    {
        SpellPool.ReturnPool(Spell);
    }

    public virtual void EffectSpell()
    {

    }
    private void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        Vector2 dir = -transform.up;
        rb.velocity = dir * Speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            HealthController health = collision.GetComponent<HealthController>();
            if (health != null)
            {
                if (CheckCollisionLayers(collision, Layers))
                {

                    EffectSpell();
                    health.DamageRecive(Damage);
                    SpellPool.ReturnPool(this.gameObject);
                }
                
            }
            else
            {
                if (!CheckCollisionLayers(collision, LayersIgnore)) 
                {
                    EffectSpell();
                    SpellPool.ReturnPool(this.gameObject);
                }
            }
        }
    }

    public bool CheckCollisionLayers(Collider2D collider, int[] allowedLayers)
    {
        int colliderLayer = collider.gameObject.layer;

 
        for (int i = 0; i < allowedLayers.Length; i++)
        {
            if (colliderLayer == allowedLayers[i])
            {
                return true; 
            }
        }

        return false;
    }
}
