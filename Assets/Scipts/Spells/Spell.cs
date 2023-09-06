using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour,SpellInterface
{
    public PoolSpell SpellPool;
    public Rigidbody2D rb;
    public float Speed;
    public float Damage;
    public float Cost;

    public virtual  GameObject CallSpell()
    {
        return SpellPool.GetObjectPool();
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

    }
}
