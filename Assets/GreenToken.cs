using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenToken : MonoBehaviour
{
   public ThirdPersonMovement myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   void OnTriggerEnter(Collider collision)
   {
      Debug.Log("Collision Detected");
      if (collision.gameObject.name == "Player")
      {
         Debug.Log("Green Token Hit");
         bool wasAdded = myPlayer.addToken(gameObject);
         if (wasAdded) Destroy(gameObject);
      }
   }
}

