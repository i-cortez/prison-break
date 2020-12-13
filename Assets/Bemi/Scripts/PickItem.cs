// PickItem.cs
//
// Ismael Cortez
// 12-10-2020
// Simple Inventory System
//
// Adapted from Sharp Coder:
// https://sharpcoderblog.com/blog/unity-3d-coding-a-simple-inventory-system-with-ui-drag-and-drop
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : MonoBehaviour
{
   // Each item must have a unique name.
   public string itemName = "My Item";
   public Texture itemPreview;

   public void PickItemUp()
   {
      Destroy(gameObject);
   }
}

