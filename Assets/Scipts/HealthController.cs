using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float MaxLife;
    private float _currentLife;
    // Start is called before the first frame update
    void Start()
    {
        _currentLife = MaxLife;
    }

    public void Damage(int damage) 
    {
        _currentLife -= damage;
        if (_currentLife <= 0) 
        {
            Death();
            return;
        } 

    }
    void Death() { }
}
