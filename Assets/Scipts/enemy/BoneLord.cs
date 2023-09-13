using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoneLord : Enemy
{
    public float cooldownDrink;
    public GameObject Gas;

    public Image CurrentLife;
    public override void StartCall()
    {
        base.StartCall();

    }
    public override void Attack()
    {
        base.Attack();
        if (Range > diffTargetDist())
        {
            _currentAttack += Time.deltaTime;

            if (_currentAttack >= CooldownAttack)
            {
                animator.SetTrigger("Attack");
                _currentAttack = 0;
                return;
            }
        }
    }
    public override void BarLife()
    {
        base.BarLife();
        if (CurrentLife == null) 
        {
            GameObject life = GameObject.FindGameObjectWithTag("BossLife");
            life.SetActive(true);
            CurrentLife = life.GetComponent<Image>();
        }
        CurrentLife.fillAmount = health._currentLife / health.MaxLife;
    }
    public override void Move()
    {
        base.Move();
        Vector2 direccion = target.transform.position - transform.position;
        Rotate(direccion);

        if (Range > diffTargetDist())
            rb.velocity *= Desacelerate;

        direccion.Normalize();

        if (Range < diffTargetDist())
            rb.velocity = direccion * Speed;
    }
}
