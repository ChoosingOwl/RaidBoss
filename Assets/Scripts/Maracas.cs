using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Maracas : MonoBehaviour {

    private TouchController scriptL;
    private TouchController scriptR;

    private GameObject lefty;
    private GameObject righty;

    private AudioSource audio;
    public AudioClip audioForward;
    public AudioClip audioBackward;

    private Rigidbody rb;
    
    private bool forward;
    private bool backward;
    


    // Use this for initialization
    void Start () {
        lefty = GameObject.Find("LeftHand");
        righty = GameObject.Find("RightHand");

        scriptL = lefty.GetComponent<TouchController>();
        scriptR = righty.GetComponent<TouchController>();

        rb = GetComponent<Rigidbody>();

        audio = GetComponent<AudioSource>();
        forward = true;
        backward = true; 

    }
	
	// Update is called once per frame
	void Update () {
        OVRInput.Update();
        
        Debug.Log("X: " + OVRInput.GetLocalControllerAngularVelocity(scriptL.controller).x + " Y: " + OVRInput.GetLocalControllerAngularVelocity(scriptL.controller).y + "Z: " + OVRInput.GetLocalControllerAngularVelocity(scriptL.controller).z);
        

        Debug.Log(OVRInput.GetLocalControllerVelocity(scriptL.controller).z);


        if (scriptL.grabbing == true && scriptL.grabbedObject.name == "maracas")
        {
            transform.localPosition = new Vector3(0.045f, 0.0138f, 0.044f);

        }


        PlaySounds();



        // If there is an increase og decrease in any xyz by 0.2f play sound. 
        // The problem right now is that float values in vectors is only showing 1 decimal, while there is alot of them. 
       


    }
    private void PlaySounds()
    {
        if (OVRInput.GetLocalControllerAngularVelocity(scriptL.controller).x < -6.0f && forward == true && scriptL.grabbedObject.name == "maracas")
        {
            audio.PlayOneShot(audioForward, 1.0f);

            forward = false;
            backward = true;
        }
        if (OVRInput.GetLocalControllerAngularVelocity(scriptL.controller).x > 6.0f && backward == true && scriptL.grabbedObject.name == "maracas")
        {
            audio.PlayOneShot(audioBackward, 1.0f);

            backward = false;
            forward = true;
        }
    }
    
    

        
    
    

}
