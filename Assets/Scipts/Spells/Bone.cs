using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : Spell
{
    public override void Move()
    {
        base.Move();
        Vector2 direccion = -transform.up;

        rb.velocity = direccion * Speed;
    }
}
