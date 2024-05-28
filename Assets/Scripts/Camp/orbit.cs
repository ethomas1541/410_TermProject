using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbit : MonoBehaviour
{
    public Transform centralObject; // The object to orbit around
    public float orbitSpeed = 10.0f; // The speed of the orbit
    public Vector3 orbitAxis = Vector3.up; // The axis to rotate around

    void Update()
    {
        // Rotate around the central object
        transform.RotateAround(centralObject.position, orbitAxis, orbitSpeed * Time.deltaTime);
    }
}
