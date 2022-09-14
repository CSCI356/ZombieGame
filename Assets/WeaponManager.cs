using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] List<Weapon> weaponsList;

    int currentWeaponIndex = 0;

    public int nextUpgradeScore = 3;

    public void UpgradeWeapon(){
        currentWeaponIndex++;

        GameObject new_weapon = weaponsList[currentWeaponIndex].weaponPrefab;

        // remove previous weapon gameobject
        GameObject childToRemove = transform.GetChild(0).gameObject;
        // childToRemove.parent = null;
        Destroy(childToRemove);

        // set new weapon gameobject
        GameObject new_weapon_instance = Instantiate(new_weapon, transform);

        new_weapon_instance.transform.parent = transform; 

        // sets the nextUpgradeScore from list
        nextUpgradeScore = weaponsList[currentWeaponIndex+1].requiredScore;
    }
}
[System.Serializable]
public class Weapon {
    public int requiredScore = 0;
    public GameObject weaponPrefab;
}
