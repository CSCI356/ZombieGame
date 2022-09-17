using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] List<GameObject> weapons;
    [SerializeField] List<int> requiredScores;

    int currentWeaponIndex = 0;

    public int nextUpgradeScore = 3;

    public void UpgradeWeapon(){
        currentWeaponIndex++;

        GameObject new_weapon = weapons[currentWeaponIndex];

        // remove previous weapon gameobject
        bool weaponObjectFound = false;
        int childIndex = 0;
        GameObject childToRemove = transform.GetChild(childIndex).gameObject;
        
        // finds the weapon object to remove
        while(!weaponObjectFound){
            childToRemove = transform.GetChild(childIndex).gameObject;
            if(childToRemove.tag == "Weapon"){
                weaponObjectFound = true;
            }
            childIndex++;
        }

        // childToRemove.parent = null;
        Destroy(childToRemove);

        // set new weapon gameobject
        GameObject new_weapon_instance = Instantiate(new_weapon, transform);

        new_weapon_instance.transform.parent = transform; 

        // sets the nextUpgradeScore from list
        nextUpgradeScore = requiredScores[currentWeaponIndex+1];
    }
}
