using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public int[] PlayerLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void Effect (GameObject o) 
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null) 
        {
            if (CheckCollisionLayers(collision, PlayerLayer)) 
            {
                Effect(collision.gameObject);
                Destroy(this.gameObject);
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
