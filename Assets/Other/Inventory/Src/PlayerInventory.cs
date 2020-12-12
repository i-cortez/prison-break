using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
   int redTokenCount = 0, greenTokenCount = 0, blueTokenCount = 0, yellowTokenCount = 0;
   const int MAXTOKENS = 6;

   public bool addRedToken()
   {
      if (redTokenCount < MAXTOKENS)
      {
         Debug.Log("A Red Token was added to inventory!");
         ++redTokenCount;
         return true;
      }

      else
      {
         Debug.Log("Could not add token!");
         return false;
      }
   }

   public void tradeRedToken()
   {

   }

   public void removeRedToken()
   {

   }

   public int getRedTokenCount()
   {
      return redTokenCount;
   }

   public bool addGreenToken()
   {
      if (greenTokenCount < MAXTOKENS)
      {
         Debug.Log("A Green Token was added to inventory!");
         ++greenTokenCount;
         return true;
      }

      else
      {
         Debug.Log("Could not add token!");
         return false;
      }
   }

   public bool addBlueToken()
   {
      if (blueTokenCount < MAXTOKENS)
      {
         Debug.Log("A Blue Token was added to inventory!");
         ++blueTokenCount;
         return true;
      }

      else
      {
         Debug.Log("Could not add token!");
         return false;
      }
   }

   public bool addYellowToken()
   {
      if (yellowTokenCount < MAXTOKENS)
      {
         Debug.Log("A Yellow Token was added to inventory!");
         ++redTokenCount;
         return true;
      }

      else
      {
         Debug.Log("Could not add token!");
         return false;
      }
   }
}

