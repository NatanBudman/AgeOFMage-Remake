using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpell : MonoBehaviour
{
    private List<GameObject> PoolList;
    public GameObject ObjectPooling;
    private int index;
    public int MaxInstance;



    void Awake()
    {
        PoolList = new List<GameObject>();
        for (int i = 0; i < MaxInstance; i++) 
        {
            GameObject Instance = Instantiate(ObjectPooling,this.transform);
            string name = $"{Instance.name}_{i} ";
            Instance.name = name;
            Instance.gameObject.SetActive(false);
            Instance.GetComponent<Spell>().SpellPool = this;

            PoolList.Add(Instance);
        }


    }

    public GameObject GetObjectPool()
    {
        GameObject ObjectPool = null;


        if (index < PoolList.Count)
        {
            if (PoolList[index] != null && PoolList[index].activeSelf == false)
            {
                ObjectPool = PoolList[index];
                index++;
            }
        }
        else 
        {
            index = 0;
        }
     
          
        if (ObjectPool == null)
        {
            ObjectPool = Instantiate(ObjectPooling, this.transform);
            ObjectPool.GetComponent<Spell>().SpellPool = this;
            PoolList.Add(ObjectPool);
            index = PoolList.Count - 1;
            index = 0;
        }
            
          

        

        ObjectPool.SetActive(true);
        return ObjectPool;
    }

    public void ReturnPool(GameObject returnoObject) 
    {
        returnoObject.SetActive(false);
    }
}

