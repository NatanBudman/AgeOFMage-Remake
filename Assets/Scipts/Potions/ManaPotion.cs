using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : Potion
{
    public float GainMana;
    public override void Effect(GameObject o)
    {
        base.Effect(o);
        base.Effect(o);
        PlayerController player = o.GetComponent<PlayerController>();
        if (player != null)
        {
            player.CurrentMana += GainMana;
            return;
        }
    }
}
