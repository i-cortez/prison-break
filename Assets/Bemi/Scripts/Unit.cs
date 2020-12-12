using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public GameObject player;

    public string playerName;
    public int movesLeft = 2;
    public bool traded;

    private bool moving;
    NavMeshAgent agent;
    float distance;

    int moveable = 0;
    Unit compare;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        moving = false;
    }

    public void movePlayer(Unit name){
        moveable = 1;
        compare = name;
        print(name.playerName + " " + name.movesLeft);
        print(compare.playerName + " " + compare.movesLeft);
    }

    // Update is called once per frame
    void Update()
    {
        if (moveable == 1) {

            if (Input.GetMouseButtonDown(0) && !moving) {
                // moving = true;
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                    moving = true;
                    var hitObject = hit.transform.gameObject.transform.position;
                    distance = Vector3.Distance(player.transform.position, hitObject);

                    if ((distance < 2.5) && (distance > 1.5)) {
                        agent.SetDestination(hitObject);
                        compare.movesLeft--;
                    }
                    moving = false;
                    moveable = 0;
                    TurnSystem.moved = true;
                    
                    // print(compare.playerName + " " + compare.movesLeft);
                }
            }
        }
    }
}
