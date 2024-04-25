using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AxeController : MonoBehaviour
{
    public Transform PlayerObject; // The object around which you want to rotate
    public float rotationSpeed = 10f; // Speed of rotation
    public float rotateDuration = 1f; // Duration for rotation
    private bool isactive = false;
    

    public void OnAttack()
    {
        if (isactive == false)
        {
            StartCoroutine(RotateAndReturn());
        }
    }

    IEnumerator RotateAndReturn()
    {
        isactive = true;
        float elapsedTime = 0f;
        while (elapsedTime < rotateDuration)
        {
            // Rotate around the target object
            transform.RotateAround(PlayerObject.position, Vector3.up, rotationSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    
        transform.Rotate(0.0f, 270.0f, 180.0f, Space.Self);
        
        elapsedTime = 0f;
        while (elapsedTime < rotateDuration)
        {
            // Rotate around the target object
            transform.RotateAround(PlayerObject.position, Vector3.up, -rotationSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        } 

         transform.rotation = Quaternion.Euler(0.0f, 90.0f, 180.0f);
        isactive = false;
    }
}