using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Room room;
    public HealthController health;
    public GameObject target;
    public Rigidbody2D rb;
    [Space]
    [Header("Stats")]
    public float Speed;
    public int Damage;
    public float Range;
    public float CooldownAttack;
    public float MaxImpulse;
    [Range(0.1f,0.9f)]public float Desacelerate;
    [HideInInspector]public float _currentAttack;

    public Animator animator;
    public bool isDeath = false;


    private void Start()
    {
        health.OnDeath += Death;
        health.OnDamage += DamageRecive;
        health.OnReavive += Revive;
        StartCall();
    }
    public virtual void StartCall() 
    {
        target = FindObjectOfType<PlayerController>().gameObject;
    }
    public virtual void DamageRecive(int impulse) 
    {
        rb.AddForce(transform.up * Mathf.Clamp(impulse,0,MaxImpulse), ForceMode2D.Force);
        return;
    }
    private void Update()
    {
        if (isDeath) 
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Move();
        Attack();
        Animations();
        BarLife();
    }

    public float diffTargetDist() 
    {
        if(target != null)
        return Vector2.Distance(target.transform.position, transform.position);

        return 0;
    }
    public virtual void Move() 
    {
       
    }
    public virtual void Animations() 
    {
    }
    public virtual void Rotate(Vector2 dir) 
    {
     
        dir.Normalize();

        // Calculamos el ángulo y rotamos el objeto hacia el objetivo
        float angulo = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angulo += 90;
        transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
    }
    public virtual void Attack() 
    {

    }
    public void Revive() 
    {
        if (room != null) room.MinionRevive();
        animator.SetTrigger("Revive");
        this.gameObject.layer = 6;
        isDeath = false;

    }
    public virtual void BarLife() 
    {

    }
    public virtual void Death() 
    {
      if(room != null)  room.MinionDeath();
        animator.SetTrigger("Death");
        this.gameObject.layer = 9;
        isDeath = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity =0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null) 
        {
            if (collision.gameObject.layer == 8) 
            {
                HealthController health = collision.GetComponent<HealthController>();

                if (health != null)
                    health.DamageRecive(Damage);
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
