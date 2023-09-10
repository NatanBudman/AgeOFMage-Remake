using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float MaxLife;
   [SerializeField]private float _currentLife;
    public SpriteRenderer render;

    public delegate void Kill();
    public Kill OnDeath;
    // Start is called before the first frame update
    void Start()
    {
        _currentLife = MaxLife;
        OnDeath += Death;
    }

    public void Damage(int damage) 
    {
        _currentLife -= damage;
        render.color = Color.red;
        StartCoroutine(DamageRecive());
        if (_currentLife <= 0) 
        {
            if (OnDeath != null) OnDeath();
            return;
        } 

    }
    IEnumerator DamageRecive() 
    {
        render.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        render.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        render.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        render.color = Color.white;
        StopCoroutine(DamageRecive());

    }

    void Death() { }
}
