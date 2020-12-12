using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    public GameObject player;

    NavMeshAgent agent;
    float distance;
    bool moving;

    TurnSystem toggle;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        moving = false;
    }

    void movePlayer() {

        if (Input.GetMouseButtonDown(0) && !moving) {
            moving = true;
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                
                var hitObject = hit.transform.gameObject.transform.position;
                distance = Vector3.Distance(player.transform.position, hitObject);

                if ((distance < 2.5) && (distance > 1.5)) {
                    agent.SetDestination(hitObject);
                }
                moving = false;
            }
        }
    }

    // Update is called once per frame
    // void Update()
    // {
    //     void playerMove() {

    //         if (Input.GetMouseButtonDown(0) && !moving) {
    //             moving = true;
    //             Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
    //             RaycastHit hit;

    //             if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                    
    //                 var hitObject = hit.transform.gameObject.transform.position;
    //                 distance = Vector3.Distance(player.transform.position, hitObject);

    //                 if ((distance < 2.5) && (distance > 1.5)) {
    //                     agent.SetDestination(hitObject);
    //                 }
    //                 moving = false;
    //             }
    //         }
    //     }
    // }
}