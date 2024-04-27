// based on https://www.youtube.com/watch?v=Ax94kLWkugg&ab_channel=JoeRickwood
// adapted by Hunter McMahon
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float MoveSpeed;
    public Vector3 offset;
    public float followDistance; 
    public float TPThreshhold = 100f;
    // Update is called once per frame
    void Update()
    {
        Vector3 pos;
        if(Vector3.Distance(transform.position, player.position) > TPThreshhold)
        {
            pos = player.position + offset + -transform.forward * followDistance;
        } else {
            pos = Vector3.Lerp(transform.position, player.position + offset + -transform.forward * followDistance, MoveSpeed * Time.deltaTime);
        }
        transform.position = pos;
    }
}
