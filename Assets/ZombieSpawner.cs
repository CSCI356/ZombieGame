using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{   
    public GameObject zombie;

    public int normalZombiesToSpawn = 3;

    public int normalZombiesSpawnedCount = 0;

    void Update()
    {   
        if(normalZombiesSpawnedCount >= normalZombiesToSpawn){
            return;
        }

        if(Random.Range(0.0f, 100.0f) < 1){
            float xPos = 40.0f;
            float zPos = 40.0f;
            
            if(Random.Range(0.0f, 100.0f) > 50){
                if(Random.Range(0.0f, 100.0f) > 50){
                    xPos = -40.0f;
                }
                zPos = Random.Range(-40.0f, 40.0f);
            }else{
                if(Random.Range(0.0f, 100.0f) > 50){
                    zPos = -40.0f;
                }
                xPos = Random.Range(-40.0f, 40.0f);
            }

            Vector3 spawnPoint = new Vector3(xPos, 0, zPos);

            Instantiate(zombie, spawnPoint, Quaternion.identity);
            normalZombiesSpawnedCount++;
        }
    }
}
