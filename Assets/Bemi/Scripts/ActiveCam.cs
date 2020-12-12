using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCam : MonoBehaviour
{
    Camera camControl;

    // Start is called before the first frame update
    void Start()
    {
        camControl = GetComponent<Camera>();
    }

    public void toggleCam(){
        camControl.enabled = !camControl.enabled;
    }

    public void toggleOff(){
        camControl.enabled = false;
    }

    public void toggleOn(){
        camControl.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
