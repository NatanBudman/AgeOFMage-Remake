using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBolt : Spell
{
  
    public override void EffectSpell()
    {
        base.EffectSpell();

    }
    public override void Move()
    {
        base.Move();
        Vector2 dir = -transform.up * Speed;
        dir.Normalize();
        rb.velocity = dir;
    }
}
