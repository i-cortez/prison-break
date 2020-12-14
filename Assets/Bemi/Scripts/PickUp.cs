using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    string itemName;
    GameObject token;
    Inventory invRef;

    void Start() {
        itemName = gameObject.name;
        token = gameObject;
    }

    void OnTriggerEnter(Collider col) {
        if ((col.tag == "Prisoner 1") || (col.tag == "Prisoner 2") ||
            (col.tag == "Prisoner 3") || (col.tag == "Prisoner 4")) {
            PickItemUp(col);
        }
    }

    void PickItemUp(Collider col) {
        print("Collected " + itemName);
        invRef = col.GetComponent<Inventory>();
        //LIMIT TO ONLY AFTER WARDENS INITIAL TURN
        if (invRef.checkInv() == 0) {

            switch(itemName) {
                case "Red Token(Clone)":
                    print("Checkpoint 1");
                    //Instantiate (invRef.redIcon, invRef.slots[invRef.nextSlot + 1]);
                    invRef.addToken(invRef.redIcon, invRef.slots[invRef.nextSlot + 1]);
                    break;
                case "Yellow Token(Clone)":
                    //Instantiate (invRef.yellowIcon, invRef.slots[invRef.nextSlot + 1]);
                    invRef.addToken(invRef.yellowIcon, invRef.slots[invRef.nextSlot + 1]);
                    break;
                case "Green Token(Clone)":
                    //Instantiate (invRef.greenIcon, invRef.slots[invRef.nextSlot + 1]);
                    invRef.addToken(invRef.greenIcon, invRef.slots[invRef.nextSlot + 1]);
                    break;
                case "Blue Token(Clone)":
                    //Instantiate (invRef.blueIcon, invRef.slots[invRef.nextSlot + 1]);
                    invRef.addToken(invRef.blueIcon, invRef.slots[invRef.nextSlot + 1]);
                    break;
                default:
                    break;
            }
            invRef.tokenNames[invRef.nextSlot] = col.name;
            invRef.availableSlots[invRef.nextSlot] = 1;
            invRef.nextSlot++;

            Destroy(gameObject);
        }else {
            print("Inventory is full!");
        }
    }
}
