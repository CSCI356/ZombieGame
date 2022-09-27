using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int kills = 0;

    [SerializeField] ZombieSpawner zombieSpawner;
    private int currentWave = 1;
    [SerializeField] private int waveAmount = 3;
    [SerializeField] private int waveMultiplierIncrease = 2;
    [SerializeField] private int DELAY_BETWEEN_WAVES = 10;

    [SerializeField] WeaponManager weaponManager;
    private SoundFXManager soundFXManager;

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


    private void Start(){
        soundFXManager = SoundFXManager.Instance;
        StartCoroutine(StartNewWave());
    }

    public void IncreaseKills(){
        kills++;
        UIManager.Instance.UpdateKills(kills);

        if(kills == weaponManager.nextUpgradeScore){
            weaponManager.UpgradeWeapon();
        }

        if(kills == zombieSpawner.normalZombiesToSpawn){
            StartCoroutine(UIManager.Instance.WaveComplete());
            soundFXManager.PlayWaveCompleteSound();
            StartCoroutine(StartNewWave());
        }
    }

    public IEnumerator StartNewWave(){
        yield return new WaitForSeconds(DELAY_BETWEEN_WAVES);
        StartCoroutine(UIManager.Instance.WaveIncoming());
        zombieSpawner.normalZombiesToSpawn += waveAmount;
        waveAmount*=waveMultiplierIncrease;
        currentWave++;
    }

    public void GameOver(){
        UIManager.Instance.GameOver();

        // save score if its a high score
    }
}

