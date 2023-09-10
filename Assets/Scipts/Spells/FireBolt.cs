using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBolt : Spell
{
    public Collider2D[] Colliders;
    public float radius = 1;
    public GameObject ZoneImpact;
    public override void StartCall()
    {
        base.StartCall();
        Colliders = new Collider2D[15];
        ZoneImpact.transform.localScale = new Vector3(radius, radius , 0);
    }
    public override void EffectSpell()
    {
        base.EffectSpell();

        ZoneImpact.SetActive(true);

        int count = Physics2D.OverlapCircleNonAlloc(transform.position,radius,Colliders);

        if (count < 0) return;

        for (int i = 0; i < Colliders.Length; i++)
        {
            Collider2D collider = Colliders[i];
            if (collider != null) 
            {
                if (CheckCollisionLayers(collider, Layers)) 
                {
                    HealthController heatlh = collider.GetComponent<HealthController>();
                    if (heatlh != null) 
                    heatlh.Damage(Damage);
                }
            }
        }
        Invoke("ZoneImpactEnable", 2.5f);
        return;
    }

    void ZoneImpactEnable() 
    {
        ZoneImpact.SetActive(!ZoneImpact.gameObject.activeInHierarchy);

    }
    public override void Move()
    {
        base.Move();
    }



    
}
