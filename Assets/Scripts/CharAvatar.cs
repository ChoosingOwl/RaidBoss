using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAvatar : MonoBehaviour {

    
    public GameObject eyes;
    public GameObject hips;

    public Vector3 fixedForward;
   
    // Use this for initialization
    void Start () {

        eyes = GameObject.Find("CenterEyeAnchor");
        hips = GameObject.Find("xbot/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2");

        fixedForward = new Vector3(0,0,1);
 
    }

    // Update is called once per frame

    void Update () {
        
        hips.transform.rotation = Quaternion.LookRotation(Vector3.Lerp(fixedForward, transform.forward, 0.1f));

        transform.rotation = eyes.transform.rotation;

  
    }
}
