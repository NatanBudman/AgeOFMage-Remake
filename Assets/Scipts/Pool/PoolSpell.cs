using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpell : MonoBehaviour
{
    private List<GameObject> PoolList = new List<GameObject>();
    public GameObject ObjectPooling;

    public int MaxInstance;



    void Start()
    {
        for (int i = 0; i < MaxInstance; i++) 
        {
            GameObject Instance = Instantiate(ObjectPooling,this.transform);
            Instance.gameObject.SetActive(false);
            Instance.GetComponent<Spell>().SpellPool = this;

            PoolList.Add(Instance);
        }


    }

    public GameObject GetObjectPool() 
    {
        GameObject ObjectPool = null;

        foreach (var gameObject in PoolList) 
        {
            if (gameObject != null) 
            {
                if (gameObject.activeInHierarchy == false) ObjectPool = gameObject;
            }
        }

        if (ObjectPool == null) ObjectPool = Instantiate(ObjectPooling,this.transform);

        return ObjectPool;
    }

    public void ReturnPool(GameObject returnoObject) 
    {
        returnoObject.SetActive(false);
    }
}

