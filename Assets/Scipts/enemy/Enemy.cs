using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   [HideInInspector] public Room room;
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
        target = FindObjectOfType<PlayerController>().gameObject;
        health.OnDeath += Death;
        health.OnDamage += DamageRecive;
        StartCall();
    }
    public virtual void StartCall() 
    {

    }
    public virtual void DamageRecive(int impulse) 
    {
        rb.AddForce(-transform.right * Mathf.Clamp(impulse,0,MaxImpulse), ForceMode2D.Impulse);
        return;
    }
    private void Update()
    {
        if (isDeath) 
            return;

        Move();
        Attack();
        Animations();
    }

    public float diffTargetDist() 
    {
        if(target != null)
        return Vector2.Distance(target.transform.position, transform.position);

        return 0;
    }
    public virtual void Move() 
    {
        Vector2 direccion = target.transform.position - transform.position;

        Rotate(direccion);

        if (Range > diffTargetDist()) 
        {
            rb.velocity *= Desacelerate;
            return;
        }
        direccion.Normalize();

        // Aplicamos una velocidad lineal al Rigidbody2D
        rb.velocity = direccion * Speed;
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
    public virtual void Death() 
    {
        room.MinionDeath();
        animator.SetTrigger("Death");
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        isDeath = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity =0;
        health.OnDeath -= Death;
    }
}
