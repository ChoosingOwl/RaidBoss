using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maracas : MonoBehaviour {

    private TouchController scriptL;
    private TouchController scriptR;

    private GameObject lefty;
    private GameObject righty;

    private Rigidbody rb;
    private Vector3 speed;

    

    public float speedInThatDirection;

    private AudioSource audio;
    


    // Use this for initialization
    void Start () {
        lefty = GameObject.Find("LeftHand");
        righty = GameObject.Find("RightHand");

        scriptL = lefty.GetComponent<TouchController>();
        scriptR = righty.GetComponent<TouchController>();

        rb = GetComponent<Rigidbody>();

        speed = OVRInput.GetLocalControllerVelocity(scriptL.controller);
        audio = GetComponent<AudioSource>();
       

    }
	
	// Update is called once per frame
	void Update () {
        OVRInput.Update();
        

        if (scriptL.grabbing == true && scriptL.grabbedObject.name == "maracas")
        {
            transform.localPosition = new Vector3(0.045f, 0.0138f, 0.044f);

        }
        Debug.Log(speed);
        if (speed != OVRInput.GetLocalControllerVelocity(scriptL.controller))
        {
            Debug.Log("ITS WORKING!");
        }
        // If there is an increase og decrease in any xyz by 0.2f play sound. 
        // The problem right now is that float values in vectors is only showing 1 decimal, while there is alot of them. 
       


    }
    
    

        
    
    

}
