using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SpellInterface
{
    GameObject CallSpell();
    void DisableSpell(GameObject Spell);
    void EffectSpell();

}
