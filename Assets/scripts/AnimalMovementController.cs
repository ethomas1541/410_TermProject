using System;
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

    // Positions to hop between - these are automatically determined
    private Vector3 start;
    private Vector3 dest;

    // Hop is currently in progress
    private bool hopping = false;

    // Current interpolation state of hop
    // 0 = not started
    // 1 = done, at destination vector
    private float hopInterp = 0;

    // Rotation correction
    // Instantiated here to save calculation time
    private Vector3 left90 = new Vector3(0, -90, 0);

    // Sometimes animals may clip a little too high
    // Set this variable to set them lower to the ground
    public float animalOffsetY;

    // Just this offset ^^^^ wrapped in zeroes (0, offset, 0)
    private Vector3 animalOffsetVector;

    public float hopHeight;

    // Start is called before the first frame update
    void Start()
    {
        parentTF=parent.transform;
        framesTilNextHop=hopIntervalFrames + UnityEngine.Random.Range(-10, 10);
        start=parentTF.position;
        this.transform.SetParent(null, true);
        animalOffsetVector = new Vector3(0, animalOffsetY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(!hopping){
            // Decrement hop timer
            framesTilNextHop--;
            if(framesTilNextHop<=0){
                hopping=true;
                hopInterp=0;
                // Get destination vector
                dest=parentTF.position + animalOffsetVector;
            }
        }else{
            // Debug.Log("hoppin");
            // Interpolate
            // The added vector determines vertical positioning.
            // It follows the parabola -ax^2 + ax where a is hopHeight, for a "realistic" trajectory
            this.transform.position=Vector3.Lerp(start, dest, hopInterp) + new Vector3(0, (-hopHeight * (float)Math.Pow(hopInterp, 2) + hopHeight * hopInterp), 0);
            // Continue interpolation if it's incomplete
            if(hopInterp<1){
                hopInterp += hopSpeed;
            // New start vector is old destination vector
            }else{
                start=dest;
                hopping=false;
                // Reset hop timer
                framesTilNextHop=hopIntervalFrames;
            }
        }
        // Rotate animal to face player regardless of hopping state
        // If we take hop state into account, you get some weird-ass squirrel drifting
        this.transform.rotation=parentTF.rotation;
        this.transform.Rotate(left90);
    }
}
