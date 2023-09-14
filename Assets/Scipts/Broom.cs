using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : MonoBehaviour
{
    public int[] EnemyLayer;
    public int[] Enemy;
    public int damage;

    public GameObject[] Loot;
    [Range(0,100)]public int PorcentageLoot;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null) 
        {
            
            if (CheckCollisionLayers(collision, EnemyLayer)) 
            {

               GameObject item  = RulleteLoot(PorcentageLoot, Loot);

                if (item != null) 
                    Instantiate(item, collision.transform.position,Quaternion.identity);

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

    private GameObject RulleteLoot(int porcentage , GameObject[] list) 
    {
        int random = Random.Range(0, 100);

        if (random > porcentage) return null;


        int randomItem = Random.Range(0, Loot.Length);

        return list[randomItem];

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
