using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomParameters roomParameters;

    private int _currentEnemy;

    public GameObject AreaSpawn;

    private float currTimeToStart;
    private float currTimeSpawn;
    private float CurrentTotalEnemy;
    private void Start()
    {
        CurrentTotalEnemy = roomParameters.TotalEnemy;
    }

    private void Update()
    {

        if (currTimeToStart < roomParameters.TimeStartRoom) 
        {
            currTimeToStart += Time.deltaTime;
            return;
        }

        SpawnMinions();
    }

    void SpawnMinions() 
    {
        if (CurrentTotalEnemy <= 0) return;

        currTimeSpawn += Time.deltaTime;
        if (roomParameters.CooldownSpawnEnemy > currTimeSpawn) return;

    

        Invoke("CallAreaSpawn",0.1f);
        Invoke("IncokeSoldaries", 0.5f);
        Invoke("CallAreaSpawn", 3f);

        currTimeSpawn = 0;
    }
    void IncokeSoldaries() 
    {
        int random = Random.Range(0, roomParameters.EnemyList.Length - 1);
        int randomInvocation = Random.Range(1, 3);

        for (int i = 0; i < randomInvocation; i++)
        {
            GameObject Instanciate = Instantiate(roomParameters.EnemyList[random], RandomArea(), Quaternion.identity);
            Instanciate.GetComponent<Enemy>().target = roomParameters.target;
            CurrentTotalEnemy--;
        }
    }

    Vector2 RandomArea() 
    {
        float randomx = Random.Range(0, 3);
        float randomy = Random.Range(0, 3);
        Vector2 area = new Vector2(AreaSpawn.transform.position.x + randomx, AreaSpawn.transform.position.y + randomy);
        return area;
    }
    void CallAreaSpawn() 
    {
        int random = Random.Range(0, roomParameters.Spawns.Length );
        AreaSpawn.transform.position = roomParameters.Spawns[random].position;
        AreaSpawn.gameObject.SetActive(!AreaSpawn.gameObject.activeInHierarchy);
    }
}
[System.Serializable]
public struct RoomParameters
{
    public GameObject[] EnemyList;
    public Transform[] Spawns;
    [Space]
    public int TotalEnemy;
    [Space]
    public int MaxEnemyInRoom;
    public int CooldownSpawnEnemy;
    public int TimeStartRoom;

    public GameObject target;

}
