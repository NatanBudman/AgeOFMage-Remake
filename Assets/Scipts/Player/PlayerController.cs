using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour
{
    PlayerModel model;
    InputManager Inputs;
    PlayerSpell Spells;


    // Spell
    int indexSpell = 0;
    GameObject currSpell;

    public event System.Action<Vector2> OnMovement;
    void Start()
    {
        model = GetComponent<PlayerModel>();
        Inputs = GetComponent<InputManager>();
        Spells = GetComponent<PlayerSpell>();
        OnMovement += model.Move;

        Spell(indexSpell);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotation();
        SpellController();
    }

    public void Move()
    {
        if (OnMovement == null) return;


        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        if (h == 0 && v == 0) 
        {
            OnMovement(Vector2.zero);
            return;
        }

        Vector3 Direction = new Vector3(h, v, 0).normalized;

        OnMovement(Direction);
    }
    public void Rotation() 
    {
        model.MouseRotation();
    }
    public void Spell(int Spell) 
    {
        currSpell = Spells.Spell[Spell];
    }
    void ChangeSpell(int spell) 
    {
        indexSpell++;
        if (indexSpell <= Spells.Spell.Length) return;
        Spell(indexSpell);
    }

    void SpellController() 
    {
        if(Input.GetKeyDown(Inputs.ChangeSpell))
            ChangeSpell(1);
    }
}
