using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class AnimalMovementController : MonoBehaviour
{
    public GameObject parent;
    // Hop speed of 0 means no hopping at all, squirrel will stay locked at its origin
    // Hop speed of 1 is teleporting and is so terrifying I'm never using it
    public float hopSpeed;
    // Frames to wait between hops, determines frequency of hops
    // FYI - a hop should not be able to start during a hop in progress
    public int hopIntervalFrames;
    private Transform parentTF;
    // Counter for determining if I'm ready to hop again - resets to hopIntervalFrames when it decrements to 0
    private int framesTilNextHop;

    // Positions to hop between
    private Vector3 start;
    private Vector3 dest;

    // Hop is currently in progress
    private bool hopping = false;

    private int hopInterp = 0;

    private Vector3 left90 = new Vector3(0, -90, 0);

    public float animalOffsetY;

    private Vector3 animalOffsetVector;

    // Start is called before the first frame update
    void Start()
    {
        parentTF=parent.transform;
        framesTilNextHop=hopIntervalFrames;
        start=parentTF.position;
        this.transform.SetParent(null, true);
        animalOffsetVector = new Vector3(0, animalOffsetY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(!hopping){
            framesTilNextHop--;
            if(framesTilNextHop==0){
                hopping=true;
                dest=parentTF.position + animalOffsetVector;
            }
        }else{
            // Debug.Log("hoppin");
            this.transform.position=dest;
            this.transform.rotation=parentTF.rotation;
            this.transform.Rotate(left90);
            framesTilNextHop=hopIntervalFrames;
            hopping=false;
        }
    }
}
