using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GetPickupItem : MonoBehaviour
{
    
    public GameObject item;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    public GameObject GetPickedItem()
    {
        return item;
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
            transform.GetChild(0).GetComponent<Renderer>().material.SetFloat("_IsEmission", 1.0f);

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
            transform.GetChild(0).GetComponent<Renderer>().material.SetFloat("_IsEmission", 0.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}