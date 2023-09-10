using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelenton : Enemy
{
    public GameObject Bullet;
    public PoolSpell Pool;
    public Transform SpawnBullet;

    public bool isCanShoot;
    public override void StartCall()
    {
        base.StartCall();
        Pool = GameObject.FindGameObjectWithTag("PoolBone").GetComponent<PoolSpell>();
    }
    public override void Animations()
    {
        base.Animations();

        animator.SetFloat("vel", rb.velocity.magnitude);
    }

    public override void Attack()
    {
        base.Attack();
        if (Range > diffTargetDist())
        {
            _currentAttack += Time.deltaTime;

            if (_currentAttack >= CooldownAttack)
            {
                animator.SetTrigger("Shoot");
                Shoot();
                return;
            }
        }
    }

    public void Shoot() 
    {
        if (isCanShoot) 
        {
            GameObject bullet = Pool.GetObjectPool();
            bullet.transform.position = SpawnBullet.transform.position;
            bullet.transform.rotation = transform.rotation;
            _currentAttack = 0;
            isCanShoot = false;
            return;
        }
       
    }
}
