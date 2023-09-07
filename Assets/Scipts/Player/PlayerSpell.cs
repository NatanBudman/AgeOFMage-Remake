using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpell : MonoBehaviour
{

    public SpellStruc[] Spell;

    public Transform SpawnSpell;

    private void Awake()
    {
        int lenghht = Spell.Length;
        for (int i = 0; i < Spell.Length; i++) 
        {
            Spell[i].SpellScript = Spell[i].Spell.GetComponent<Spell>();
        }
    }

    public Spell SpellSelected(int i) 
    {
        return Spell[i].SpellPool.GetObjectPool().GetComponent<Spell>(); ;
    }
    public PoolSpell PoolSelected(int i)
    {
        return Spell[i].SpellPool;
    }
}

[System.Serializable]
public struct SpellStruc 
{
    public string SpellName; 

    public GameObject Spell;

    public Spell SpellScript;

    public PoolSpell SpellPool;
}
