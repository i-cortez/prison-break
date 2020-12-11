using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Token", menuName = "Inventory/Token")]

public class Token : ScriptableObject
{
   new public string name = "Token";
   public Sprite icon = null;
}

