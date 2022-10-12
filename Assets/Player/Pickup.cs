using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    public int placeableItemCount = 0;
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

       // LayerMask mask = LayerMask.GetMask("Item", weaponTag);

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
            // run pickup if the tag is Item and placeableItem is null (empty)
            else if ((Input.GetKeyDown(pickupKey)) && hit.transform.CompareTag("Item"))
            {
                pickup(hit);
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

    void pickup(RaycastHit hit)
    {
        placeableItem = hit.transform.gameObject.GetComponent<GetPickupItem>().GetPickedItem();

        placeableItemCount += 5;
        UIManager.Instance.UpdateWallCount(placeableItemCount);
        SoundFXManager.Instance.PlayCollectibleSound();

        //destory the pickup item from the world
        Destroy(hit.transform.gameObject);
    }

    void placeItem()
    {
        print("place Item");
        // the amount of units in front of the player the object will be created 
        float units = 0.02f;

        // if player has an item to place (wall)
        if (placeableItem != null && placeableItemCount > 0)
        {
            float playerRotation = transform.eulerAngles.y;

            float itemFace = 0;

            if (playerRotation > 45 && playerRotation < 135)
                itemFace = 90;
            else if (playerRotation > 135 && playerRotation < 225)
                itemFace = 180;
            else if (playerRotation > 225 && playerRotation < 315)
                itemFace = 270;
            Debug.Log(playerRotation);

            Quaternion new_rotation = Quaternion.Euler(0, itemFace, 0);
            
            // place object near player, thats some units ahead on the z axis     
            Instantiate(placeableItem, transform.TransformPoint(new Vector3(0, 0, units)), new_rotation);
            placeableItemCount -=1;
            UIManager.Instance.UpdateWallCount(placeableItemCount);
            SoundFXManager.Instance.PlayWalkingSound();
        }
    }
}