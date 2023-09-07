using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpell : MonoBehaviour
{
    public List<GameObject> PoolList;
    public GameObject ObjectPooling;

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
        Debug.Log("hola");
        GameObject ObjectPool = null;

        int length = PoolList.Count;
        for (int i = 0; i < length; i++)
        {
            if (PoolList[i] != null && PoolList[i].activeSelf == false)
            {
                ObjectPool = PoolList[i];
                break;
            }
        }

        if (ObjectPool == null)
        {
            ObjectPool = Instantiate(ObjectPooling, this.transform);
            PoolList.Add(ObjectPool);
        }
        ObjectPool.SetActive(true);

        return ObjectPool;
    }

    public void ReturnPool(GameObject returnoObject) 
    {
        returnoObject.SetActive(false);
    }
}

