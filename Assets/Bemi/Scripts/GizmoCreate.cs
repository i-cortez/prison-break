using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoCreate : MonoBehaviour
{
    public GameObject cell;

    public float radius;

    public int redLimit = 2;
    public int yellowLimit = 2;
    public int greenLimit = 2;
    public int blueLimit = 2;

    void Start(){
        if (cell.name == "Red Cell") {
            redLimit = 0;
        }

        if (cell.name == "Yellow Cell") {
            yellowLimit = 0;
        }

        if (cell.name == "Green Cell") {
            greenLimit = 0;
        }

        if (cell.name == "Blue Cell") {
            blueLimit = 0;
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
