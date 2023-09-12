using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : MonoBehaviour
{
    public int[] EnemyLayer;
    public int[] Enemy;
    public int damage;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null) 
        {
            
            Debug.Log(collision.gameObject.name);
            if (CheckCollisionLayers(collision, EnemyLayer)) 
            {
                Destroy(collision.gameObject);
            }
            if (CheckCollisionLayers(collision, Enemy))
            {
                var healthController = collision.GetComponent<HealthController>();
                if (healthController != null)
                {
                    healthController.DamageRecive(damage);
                }
            }
        }
    }

    public bool CheckCollisionLayers(Collider2D collider, int[] allowedLayers)
    {
        int colliderLayer = collider.gameObject.layer;


        for (int i = 0; i < allowedLayers.Length; i++)
        {
            if (colliderLayer == allowedLayers[i])
            {
                return true;
            }
        }

        return false;
    }
}
