using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnItem;
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField]int spawnCount;
    int currentCount;
    public string objectName;
    bool active;
    void Start()
    {   
        StartSpawn();
        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawn");  
    }

    void StartSpawn(){
         if(spawnItem == null){
            return;
        }
        else
        StartCoroutine(SpawnProcess(spawnCount));
    }
    IEnumerator SpawnProcess(int n)
    {
            yield return new WaitForSeconds(5f);

        for (int i = 0; i < n; i++)
        {
            SpawnThing(currentCount);
            yield return new WaitForSeconds(.75f);
        }
    }
    void SpawnThing(int id){
        int point = Random.Range(0, spawnPoints.Length);
        GameObject spawnedItem = Instantiate(spawnItem, spawnPoints[point].transform.position, spawnPoints[point].transform.rotation);
        if(objectName!=null){
            spawnedItem.name = $"{objectName} {id}";
        } else{
            spawnedItem.name = $"{gameObject.name} Item {id}";
        }
        currentCount++;
    }
}
