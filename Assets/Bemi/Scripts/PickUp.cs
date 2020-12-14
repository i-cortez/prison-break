using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    string itemName;
    string owner;
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

        if (col.tag == "Inventory") {
            print("Another Checkpoint");
            PickItemUp(col);
        }
    }

    void PickItemUp(Collider col) {
        print("Collected " + itemName);
        invRef = col.gameObject.GetComponent<Inventory>();
        print(invRef.name);

        if (invRef.checkInv() == 0) {

            switch(itemName) {

                case "Red Token":
                    print("Checkpoint 1");
                    //Instantiate (invRef.redIcon, invRef.slots[invRef.nextSlot + 1]);
                    invRef.addToken(invRef.redIcon, invRef.slots[invRef.nextSlot + 1]);
                    invRef.tokenNames[invRef.nextSlot] = "Red Token";
                    break;
                case "Yellow Token":
                    //Instantiate (invRef.yellowIcon, invRef.slots[invRef.nextSlot + 1]);
                    invRef.addToken(invRef.yellowIcon, invRef.slots[invRef.nextSlot + 1]);
                    invRef.tokenNames[invRef.nextSlot] = "Yellow Token";
                    break;
                case "Green Token":
                    //Instantiate (invRef.greenIcon, invRef.slots[invRef.nextSlot + 1]);
                    invRef.addToken(invRef.greenIcon, invRef.slots[invRef.nextSlot + 1]);
                    invRef.tokenNames[invRef.nextSlot] = "Green Token";
                    break;
                case "Blue Token":
                    //Instantiate (invRef.blueIcon, invRef.slots[invRef.nextSlot + 1]);
                    invRef.addToken(invRef.blueIcon, invRef.slots[invRef.nextSlot + 1]);
                    invRef.tokenNames[invRef.nextSlot] = "Blue Token";
                    break;
                case "Red Token(Clone)":
                    print("Checkpoint 1");
                    //Instantiate (invRef.redIcon, invRef.slots[invRef.nextSlot + 1]);
                    invRef.addToken(invRef.redIcon, invRef.slots[invRef.nextSlot + 1]);
                    invRef.tokenNames[invRef.nextSlot] = "Red Token";
                    break;
                case "Yellow Token(Clone)":
                    //Instantiate (invRef.yellowIcon, invRef.slots[invRef.nextSlot + 1]);
                    invRef.addToken(invRef.yellowIcon, invRef.slots[invRef.nextSlot + 1]);
                    invRef.tokenNames[invRef.nextSlot] = "Yellow Token";
                    break;
                case "Green Token(Clone)":
                    //Instantiate (invRef.greenIcon, invRef.slots[invRef.nextSlot + 1]);
                    invRef.addToken(invRef.greenIcon, invRef.slots[invRef.nextSlot + 1]);
                    invRef.tokenNames[invRef.nextSlot] = "Green Token";
                    break;
                case "Blue Token(Clone)":
                    //Instantiate (invRef.blueIcon, invRef.slots[invRef.nextSlot + 1]);
                    invRef.addToken(invRef.blueIcon, invRef.slots[invRef.nextSlot + 1]);
                    invRef.tokenNames[invRef.nextSlot] = "Blue Token";
                    break;
                default:
                    break;
            }
            owner = name;
            invRef.availableSlots[invRef.nextSlot] = 1;
            invRef.nextSlot++;

            Destroy(gameObject);
        }else {
            print("Inventory is full!");
        }
    }
}
