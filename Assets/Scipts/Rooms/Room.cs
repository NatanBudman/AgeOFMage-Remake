using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("RoomParamters")]
    public RoomParameters roomParameters;
    [Header("WavesParamters")]
    public Wave[] Waves;
    private Wave currentWave;
    private int indexWave;
    [Space]
    [Space]

    public GameObject AreaSpawn;

    private float currTimeToStart;
    private float currTimeSpawn;
    private float CurrentTotalEnemy;
    private float CurrentEnemyInScene;
    private void Start()
    {
        currentWave = Waves[0];
        CurrentTotalEnemy = currentWave.Enemy;
        
    }

    private void Update()
    {

        if (currTimeToStart < roomParameters.TimeStartRoom) 
        {
            currTimeToStart += Time.deltaTime;
            return;
        }

        SpawnMinions();

        if (CurrentTotalEnemy <= 0 && CurrentEnemyInScene <= 0)
        {
            Invoke("ChangeRound", 1);
            currTimeToStart = 0;
        }
    }

    void SpawnMinions() 
    {
        if (CurrentTotalEnemy <= 0) return;

        currTimeSpawn += Time.deltaTime;
        if (roomParameters.CooldownSpawnEnemy > currTimeSpawn) return;

    

        Invoke("CallAreaSpawn",0.1f);
        Invoke("InvokeSoldaries", 0.5f);
        Invoke("CallAreaSpawn", 3f);

        currTimeSpawn = 0;
    }
    void ChangeRound() 
    {
        if (indexWave < Waves.Length - 1) indexWave++;
        else return;
        currentWave = Waves[indexWave];
        CurrentTotalEnemy = currentWave.Enemy;

    }
    void InvokeSoldaries() 
    {
        int random = Random.Range(0, currentWave.Enemies.Length - 1);
        int randomInvocation = Random.Range(1, 3);

        for (int i = 0; i < randomInvocation; i++)
        {
            if (CurrentTotalEnemy <= 0) return;
            GameObject Instanciate = Instantiate(currentWave.Enemies[random], RandomArea(), Quaternion.identity);
            Instanciate.GetComponent<Enemy>().room = this;
            CurrentEnemyInScene++;
            CurrentTotalEnemy--;
        }
    }
    public void MinionDeath()
    {
        CurrentEnemyInScene--;
    }

    Vector2 RandomArea() 
    {
        float randomx = Random.Range(0, roomParameters.RangeAreaSpawn);
        float randomy = Random.Range(0, roomParameters.RangeAreaSpawn);
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
    [Header("Spawn")]
    public Transform[] Spawns;
    [Range(1,10)]public int RangeAreaSpawn;
    [Space]
    [Header("RoomParameters")]
    public int CooldownSpawnEnemy;
    public int TimeStartRoom;

}
[System.Serializable]
public struct Wave
{
    public string RoundNumber;
    public int current;
    
    public int Enemy;

    public GameObject[] Enemies;

    public int MaxEnemiesInRoom;

}
