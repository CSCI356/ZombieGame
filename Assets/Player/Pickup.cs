using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public KeyCode PlaceKey = KeyCode.P;
    string weaponTag = "Weapon";
    string itemTag = "Item";

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
    public List<GameObject> placeableItems;

    void Update()
    {

        // SELECT WEAPONS
        // if (Input.GetKeyDown(KeyCode.Alpha1))
        // {
        //     SelectWeapon(0);
        // }

        // if (Input.GetKeyDown(KeyCode.Alpha2))
        // {
        //     SelectWeapon(1);
        // }

        // {
            
        //     if ((hit.transform.CompareTag(weaponTag)) && weapons.Count < maxWeapons))
        //     {

        //         // save the weapon                
        //         weapons.Add(hit.collider.gameObject);

        //         // hides the weapon because it's now in our 'inventory'
        //         hit.collider.gameObject.SetActive(false);

        //         // now we can positioning the weapon at many other places.
        //         // but for this demonstration where we just want to show a weapon
        //         // in our hand at some point we do it now.
        //         hit.transform.parent = hand;
        //         hit.transform.position = Vector3.zero;
        //     }
        // }

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

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected");
        if((collision.gameObject.tag == itemTag) && (collision.gameObject.name != "Heart")){
            // Increase Item count
            pickup(collision);
        }else if(collision.gameObject.tag == weaponTag){
            // do something with weapon
        }
    }

    void pickup(Collision col)
    {   
        placeableItems.Add(col.transform.gameObject.GetComponent<GetPickupItem>().GetPickedItem());

        UIManager.Instance.UpdateWallCount(placeableItems.Count);

        //destory the pickup item from the world
        Destroy(col.transform.gameObject);
    }

    void placeItem()
    {
        Debug.Log("place Item");
        // the amount of units in front of the player the object will be created 
        float units = 0.02f;

        // if player has an item to place (wall)
        if (placeableItems.Count > 0)
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
            Debug.Log(playerRotation);

            Quaternion new_rotation = Quaternion.Euler(0, itemFace, 0);

            // place object near player, thats some units ahead on the z axis     
            Instantiate(placeableItems[0], transform.TransformPoint(new Vector3(0, 0, units)), new_rotation);

            // reset placeableItem to null
            placeableItems.RemoveAt(0);

            UIManager.Instance.UpdateWallCount(placeableItems.Count);
        }
    }
}