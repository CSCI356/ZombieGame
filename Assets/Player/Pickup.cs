using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform cameraTransform;
    public KeyCode pickupKey = KeyCode.E;
    public KeyCode dropKey = KeyCode.G;
    public KeyCode PlaceKey = KeyCode.P;
    string weaponTag = "Weapon";

    public List<GameObject> weapons;
    public int maxWeapons = 2;

    // this variable represent the weapon you carry in your hand 
    public GameObject currentWeapon;

    // this variable represent your hand which you set as the parent of your currentWeapon
    public Transform hand;

    // Insert a gameobject which you drop inside your player gameobject and position it where you want to drop items from
    // to avoid dropping items inside your player
    public Transform dropPoint;


    // placeable item hold (of 1)
    public GameObject placeableItem = null;

    void Update()
    {

        // SELECT WEAPONS
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectWeapon(1);
        }

        // PICKUP WEAPONS
        RaycastHit hit;
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag(weaponTag) && Input.GetKeyDown(pickupKey) && weapons.Count < maxWeapons)
            {

                // save the weapon                
                weapons.Add(hit.collider.gameObject);

                // hides the weapon because it's now in our 'inventory'
                hit.collider.gameObject.SetActive(false);

                // now we can positioning the weapon at many other places.
                // but for this demonstration where we just want to show a weapon
                // in our hand at some point we do it now.
                hit.transform.parent = hand;
                hit.transform.position = Vector3.zero;
            }
        }

        // DROP WEAPONS
        // So let's say you wanted to add a feature where you wanted to drop the weapon you carry in your hand
        if (Input.GetKeyDown(dropKey) && currentWeapon != null)
        {

            // First ensure we remove our hand as parent for the weapon
            currentWeapon.transform.parent = null;

            // Move the weapon to the drop position
            currentWeapon.transform.position = dropPoint.position;

            // Remove it from our 'inventory'            
            var weaponInstanceId = currentWeapon.GetInstanceID();
            for (int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].GetInstanceID() == weaponInstanceId)
                {
                    weapons.RemoveAt(i);
                    break;
                }
            }

            // Remove it from our 'hand'
            currentWeapon = null;
        }

        // pickup placeable item
        // add later 

        // use placeable item
        if (Input.GetKeyDown(PlaceKey))
        {
            placeItem();
        }


    }

    void SelectWeapon(int index)
    {
        // Ensure we have a weapon in the wanted 'slot'
        if (weapons.Count > index && weapons[index] != null)
        {

            // Check if we already is carrying a weapon
            if (currentWeapon != null)
            {
                // hide the weapon                
                currentWeapon.gameObject.SetActive(false);
            }

            // Add our new weapon
            currentWeapon = weapons[index];

            // Show our new weapon
            currentWeapon.SetActive(true);
        }
    }

    // pickup items. (placeable Wall)
    void pickup()
    {
        RaycastHit hit;
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("obtainable") && Input.GetKeyDown(pickupKey))
            {
                // save the item to placeableItem               
                placeableItem = (hit.collider.gameObject);

                // hide the placeable item in the world 
                hit.collider.gameObject.SetActive(false);
            }
        }

    }
    // place item (wall)
    void placeItem()
    {
        // the amount of units in front of the player the object will be created 
        float units = 2;

        // if player has an item to place (wall)
        if (placeableItem != null)
        {
            // snap rotation to ether 0, 90, 180, 270 based on players rotation 
            float playerRotation = transform.eulerAngles.y;

            float itemFace = 0;

            if (playerRotation > 45 && playerRotation < 135)
                itemFace = 90;
            else if (playerRotation > 135 && playerRotation < 225)
                itemFace = 180;
            else if (playerRotation > 225 && playerRotation < 315)
                itemFace = 270;

            // place object near player, thats some units ahead on the x axis     
            Instantiate(placeableItem, transform.TransformPoint(new Vector3(units, 0, 0f)), new Quaternion(0.0f, itemFace, 0.0f, 0.0f));

            // reset placeableItem to null
            placeableItem = null;

        }
    }
}