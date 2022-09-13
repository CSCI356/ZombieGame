using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int kills = 0;

    [SerializeField] ZombieSpawner zombieSpawner;
    private int currentWave = 1;

    private void Awake(){
        // If there is an instance, and it's not me, delete myself
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public void IncreaseKills(){
        kills++;
        UIManager.Instance.UpdateKills(kills);

        if(kills == zombieSpawner.normalZombiesToSpawn){
            StartCoroutine(UIManager.Instance.WaveComplete());
            StartCoroutine(StartNewWave());
        }
    }

    public IEnumerator StartNewWave(){
        yield return new WaitForSeconds(10);
        StartCoroutine(UIManager.Instance.WaveIncoming());
        zombieSpawner.normalZombiesToSpawn += 3;
    }

    public void GameOver(){
        UIManager.Instance.GameOver();

        // save score if its a high score
    }
}

