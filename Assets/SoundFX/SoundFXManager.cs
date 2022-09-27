using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{   
    [SerializeField] private AudioSource walkingSound;
    [SerializeField] private AudioSource zombieSound;
    [SerializeField] private AudioSource waveCompleteSound;
    [SerializeField] private AudioSource collectibleSound;
    [SerializeField] private AudioSource damageSound;

    public static SoundFXManager Instance { get; private set; }

    private void Awake(){
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public void PlayWalkingSound(){
        walkingSound.Play();
    }

    public void PlayZombieGroanSound(){
        zombieSound.Play();
    }

    public void PlayWaveCompleteSound(){
        waveCompleteSound.Play();
    }

    public void PlayCollectibleSound(){
        collectibleSound.Play();
    }

    public void PlayDamageSound(){
        damageSound.Play();
    }
}
