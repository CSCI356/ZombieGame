using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }
    [SerializeField] List<GameObject> weapons;
    [SerializeField] List<int> requiredScores;

    [SerializeField] GameObject handObject;

    int currentWeaponIndex = 0;

    public int nextUpgradeScore = 3;

    private void Awake()
    {
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

    public void UpgradeWeapon()
    {
        currentWeaponIndex++;

        GameObject new_weapon = weapons[currentWeaponIndex];

        // remove previous weapon gameobject
        bool weaponObjectFound = false;
        int childIndex = 0;
        GameObject childToRemove = transform.GetChild(childIndex).gameObject;

        // finds the weapon object to remove
        while (!weaponObjectFound)
        {
            childToRemove = handObject.transform.GetChild(childIndex).gameObject;
            if (childToRemove.tag == "Weapon")
            {
                weaponObjectFound = true;
            }
            childIndex++;
        }

        // childToRemove.parent = null;
        Destroy(childToRemove);

        // set new weapon gameobject
        GameObject new_weapon_instance = Instantiate(new_weapon, transform);

        new_weapon_instance.transform.parent = handObject.transform;

        if (currentWeaponIndex + 1 > requiredScores.Count - 1)
        {
            return;
        }

        // sets the nextUpgradeScore from list
        nextUpgradeScore = requiredScores[currentWeaponIndex + 1];
    }

    public int getCurrentBulletDamage()
    {
        return weapons[currentWeaponIndex].GetComponent<Shoot>().bulletDamage;
    }
}
