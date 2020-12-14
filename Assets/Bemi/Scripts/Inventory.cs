using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject player;
    public GameObject inventoryPrefab;
    [Space]
    public GameObject redIcon;
    public GameObject yellowIcon;
    public GameObject greenIcon;
    public GameObject blueIcon;

    //public string test = "Successful!";
    public int nextSlot = 0;
    public int[] availableSlots = new int[6];
    public string[] tokenNames = new string[6];
    public Transform[] slots = new Transform[7];
    GameObject[] myTokens = new GameObject[6];
    GameObject hud;
    GameObject inv;
    public bool toggle = false;

    public GameObject inventory;


    void Start() {
        
        hud = GameObject.Find("HUD");
        inventory = Instantiate (inventoryPrefab, hud.transform);
        print(inventory.name);

        changeName();
        print(inventory.name);
        
        //inventory.SetActive(false);

        //print("Instantiated inventory: " + inventory.name);
        GameObject parent = GameObject.Find(inventory.name);
        //print(parent.name);

        slots = parent.GetComponentsInChildren<Transform>(); //each slots transform (index + 1)

        //set all slots to empty
        for (int i = 0; i < availableSlots.Length; ++i) {
            availableSlots[i] = 0;
        }

        shiftInv(inventory);
    }

    public int checkInv () {
        int check = 1; //assume full
        for (int i = 0; i < 6; ++i) {
            if (availableSlots[i] == 0) {
                check = 0;
            }
        }
        
        return check;
    }

    public void addToken (GameObject icon, Transform slot) {
        print("Initiated Successfully");
        Instantiate (icon, slot);
        //shift y (330, -330), x(+2250)
    }

    void changeName(){

        switch (player.tag) {

            case "Prisoner 1":
                inventory.name = "Inventory1";
                break;

            case "Prisoner 2":
                inventory.name = "Inventory2";
                break;

            case "Prisoner 3":
                inventory.name = "Inventory3";
                break;

            case "Prisoner 4":
                inventory.name = "Inventory4";
                break;

            default:
                break;
        }
    }

    //MOVE INVENTORIES TO SET LOCATIONS
    void shiftInv (GameObject inv) {

        switch (player.name) {
            
            case "Prisoner 1(Clone)":
                inv.GetComponent<RectTransform>().anchoredPosition = new Vector3(295f, 330f, 0f);
                break;
            
            case "Prisoner 2(Clone)":
                inv.GetComponent<RectTransform>().anchoredPosition = new Vector3(295f, -330f, 0f);
                break;

            case "Prisoner 3(Clone)":
                inv.GetComponent<RectTransform>().anchoredPosition = new Vector3(2250f, 330f, 0f);
                break;

            case "Prisoner 4(Clone)":
                inv.GetComponent<RectTransform>().anchoredPosition = new Vector3(2250f, -330f, 0f);
                break;
            
            default:
                break;
        }
    }

    public void invOn() {
        inventory.SetActive(true);
    }

    public void invOff() {
        inventory.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown("i")){
            toggle = !toggle;
            inventory.SetActive(toggle);
        }
    }

}
