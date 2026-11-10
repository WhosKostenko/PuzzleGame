using UnityEngine;
using System.Collections;

public class Doorutton : MonoBehaviour
{



    public GameObject Door;
    bool doormoved = false;
    // “other” refers to the collider on the GameObject inside this trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("The player character has touched the door.");

            if (doormoved == false) { 
            Vector3 pos = Door.transform.position;
                Door.transform.position = new Vector3(pos.x,pos.y+100,pos.z);

                doormoved = true;
            }
        }
    }


}