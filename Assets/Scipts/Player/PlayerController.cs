using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour
{
    PlayerModel model;
    InputManager Inputs;
    PlayerSpell Spells;
    Animator animator;


    // Spell
    int indexSpell = 0;
    Spell currSpell;
    PoolSpell pool;
    [Header("Mana")]
    public float MaxMana;
    public float CurrentMana;
    public float ManaRegenerate;
    public float ManaRegeneratePerSecond;
    public event System.Action<Vector2> OnMovement;
    void Start()
    {
        CurrentMana = MaxMana;
        animator = GetComponent<Animator>();
        model = GetComponent<PlayerModel>();
        Inputs = GetComponent<InputManager>();
        Spells = GetComponent<PlayerSpell>();
        OnMovement += model.Move;

        Spell(0);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotation();
        SpellController();
        Broom();
        Mana();
    }

    public void Move()
    {
        if (OnMovement == null) return;


        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        if (h == 0 && v == 0)
        {

            model._rb.velocity = Vector2.zero;
            model._rb.angularVelocity = 0;
            // Aplicar la fuerza
            return;
        }

        Vector3 Direction = new Vector3(h, v, 0).normalized;
        animator.SetFloat("vel",model._rb.velocity.magnitude);
        OnMovement(Direction);
    }
    public void Rotation() 
    {
        model.MouseRotation();
    }
    public void Spell(int Spell) 
    {
        currSpell = Spells.SpellSelected(Spell);
        pool = Spells.PoolSelected(Spell);
    }
    void ChangeSpell(int spell) 
    {
        indexSpell = spell;
        if (indexSpell > Spells.Spell.Length || indexSpell < 0  ) return;
        Spell(indexSpell);
    }

    void SpellController() 
    {
        int lenght = Inputs.ChangeSpell.Length;
        for (int i = 0; i < lenght; i++) 
        {
           if(Input.GetKeyDown(Inputs.ChangeSpell[i]))
               ChangeSpell(i);
        }

        if (Input.GetKeyDown(Inputs.FireSpell) && CurrentMana >= currSpell.Cost) 
        {
            GameObject MagicSpell = pool.GetObjectPool();
            MagicSpell.transform.position = Spells.SpawnSpell.position;
            MagicSpell.transform.rotation = Spells.SpawnSpell.rotation;
            CurrentMana -= currSpell.Cost;
            return;
        }
      
    }

    void Broom() 
    {
        if (Input.GetKeyDown(Inputs.Broom))
        {
            animator.SetTrigger("Broom");
        }
    }

    void Mana() 
    {
        if (CurrentMana < MaxMana)
        {
            StartCoroutine(ManaRegenarate());
        }
        else 
        {
            StopCoroutine(ManaRegenarate());
        }
    }
    IEnumerator ManaRegenarate() 
    {
        CurrentMana += ManaRegenerate * Time.deltaTime;
        yield return new WaitForSeconds(ManaRegeneratePerSecond);
        StopCoroutine(ManaRegenarate());
    }
}
