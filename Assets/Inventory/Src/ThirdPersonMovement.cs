using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
   public CharacterController myController;
   public Transform myCam;
   public PlayerInventory myInventory;

   public float moveSpeed = 6.0f;
   public float smoothTurnTime = 0.1f;
   float smoothTurnVelocity;



   // Update is called once per frame
   void Update()
   {
      float horizontal = Input.GetAxisRaw("Horizontal");
      float vertical = Input.GetAxisRaw("Vertical");

      Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;

      if(direction.magnitude >= 0.1f)
      {
         float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + myCam.eulerAngles.y;
         float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTurnVelocity, smoothTurnTime);

         transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

         Vector3 moveDir = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
         myController.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
      }
   }

   public bool addToken(GameObject Obj)
   {
      if(Obj.tag == "Red Token")
      {
         return myInventory.addRedToken();
      }

      if (Obj.tag == "Green Token")
      {
         return myInventory.addGreenToken();
      }

      if (Obj.tag == "Blue Token")
      {
         return myInventory.addBlueToken();
      }

      if (Obj.tag == "Yellow Token")
      {
         return myInventory.addYellowToken();
      }

      return false;
   }
}

