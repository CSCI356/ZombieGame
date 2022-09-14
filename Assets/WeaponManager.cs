using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] List<GameObject> weapons;
    [SerializeField] List<int> scores;

    int currentWeaponIndex = 0;

    public int nextUpgradeScore = 3;

    public void UpgradeWeapon(){
        currentWeaponIndex++;

        GameObject new_weapon = weapons[currentWeaponIndex];

        // remove previous weapon gameobject
        GameObject childToRemove = transform.GetChild(0).gameObject;
        // childToRemove.parent = null;
        Destroy(childToRemove);

        // set new weapon gameobject
        GameObject new_weapon_instance = Instantiate(new_weapon, transform);

        new_weapon_instance.transform.parent = transform; 

        // sets the nextUpgradeScore from list
        nextUpgradeScore = scores[currentWeaponIndex+1];
    }
}
