using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Potion
{
    public float GainLife;
    public override void Effect(GameObject o)
    {
        base.Effect(o);
        HealthController health = o.GetComponent<HealthController>();
        if (health != null) 
        {
            health.AddLife(GainLife);
            return;
        }

    }
}
