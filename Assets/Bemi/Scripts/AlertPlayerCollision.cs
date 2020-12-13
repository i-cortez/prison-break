using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertPlayerCollision : MonoBehaviour
{
    public GameObject myPlayer;

    void OnTriggerEnter(Collider collisionObj)
    {
        Debug.Log(myPlayer.tag + " colided with " + collisionObj.tag);

        if(collisionObj.tag == "Token")
        {
            // Add the token to the players inventory
            myPlayer.GetComponent<InventorySystem>().addItem(collisionObj.gameObject);
        }
    }
}

