using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
   public GameObject inventoryUI;
   public PlayerInventory myInventory;
   Text txt;

   // Start is called before the first frame update
   void Start()
   {

   }
   
   // Update is called once per frame
   void Update()
   {
      if (Input.GetButtonDown("Inventory"))
      {
         inventoryUI.SetActive(!inventoryUI.activeSelf);
      }

   }

   public void refreshInventoryUI()
   {
      // refresh the inventory numbers
   }
}

