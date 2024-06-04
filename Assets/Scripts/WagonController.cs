using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonController : MonoBehaviour
{
    public List<GameObject> wheels;
    public float turn_degrees_per_frame;

    public bool spinning;

    // Update is called once per frame
    void FixedUpdate(){
        if(spinning){
            foreach(GameObject wheel in wheels){
                wheel.transform.Rotate(0, 0, -turn_degrees_per_frame);
            }
        }
    }
}
