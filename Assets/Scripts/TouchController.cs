using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TouchController : MonoBehaviour {

    private Animator _anim;
    public GameObject grabbedObject;
    public bool grabbing;

    public float grabRadius;
    public LayerMask grabMask;

    public OVRInput.Controller controller;
    public string buttonName;

    public Vector3 velo;


	// Use this for initialization
	void Start () {

        _anim = GetComponentInChildren<Animator>();
        
    }
	
	// Update is called once per frame
	void Update () {
        OVRInput.Update();

        velo = OVRInput.GetLocalControllerVelocity(controller);

        

        //if you are not grabbing (!) and the trigger is squished, then grab. If you are grabbing and the trigger is released then drop object.
        if (!grabbing && Input.GetAxis(buttonName) > 0.75f) GrabObject();
        if (grabbing && Input.GetAxis(buttonName) < 0.75f) DropObject();



        LeftGrabAnimation();
        RightGrabAnimation();

        
    }

    void GrabObject()
    {
        
        //the spherecast is going to return an array of hits (array[])
        RaycastHit[] hits;
        /*The position of the spherecast is from the hands and the script is attached to the hands therefore it is enough with transform.position, the radius of the sphere,transform.forward
         * does not matter as we just want it to appear, max distances does not matter, and grabMask is for colliders for only special layers*/
        hits = Physics.SphereCastAll(transform.position, grabRadius, transform.forward, 0f, grabMask);
        //if we does not hit something the array is at 0 length. We do not want anything to happen at 0 length, hence the following
        if(hits.Length > 0)
        {
            grabbing = true;
            int closestHit = 0;
            //we are going to loop through our hits array and check the one that is closest
            for (int i = 0; i < hits.Length; i++)
            {
                //the hit we are looking at is that the distance from the center of the sphere to whereever that collider was.
                if (hits[i].distance < hits[closestHit].distance) closestHit = i;
            }
            // This is now the closestHit collider and we therefore know the closest object with that certain collider
            grabbedObject = hits[closestHit].transform.gameObject;
            //all objects have rigidbody, but we do not want the objects to have rigidbody as they should follow the physics of the grabbing hand. We can do so by kinematic.
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            //Move the object to the position of the hand
           // grabbedObject.transform.position = transform.position;

            //lock the grabbed object to the fist - setting the grabbed object as a child. 
            grabbedObject.transform.parent = transform;
        }

    }

    void DropObject()
    {
        grabbing = false;

        //we want to check that we actually have an grabbed object
        if(grabbedObject != null)
        {
            grabbedObject.transform.parent = null;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(controller);
            grabbedObject.GetComponent<Rigidbody>().angularVelocity = OVRInput.GetLocalControllerAngularVelocity(controller);

            
            grabbedObject = null;
        }
    }
    void LeftGrabAnimation()
    {
        if (OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) >= 0.75f)
        {
            if (!_anim.GetBool("IsGrabbing"))
            {
                _anim.SetBool("IsGrabbing", true);

            }
        }

        else
        {

            if (_anim.GetBool("IsGrabbing"))
            {
                _anim.SetBool("IsGrabbing", false);
            }

        }
    }
    void RightGrabAnimation()
    {
        if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) >= 0.75f)
        {
            if (!_anim.GetBool("RightIsGrabbing"))
            {
                _anim.SetBool("RightIsGrabbing", true);

            }
        }

        else
        {

            if (_anim.GetBool("RightIsGrabbing"))
            {
                _anim.SetBool("RightIsGrabbing", false);
            }

        }
    }






}
