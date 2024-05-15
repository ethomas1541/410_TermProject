// 5/15/24 - adding respawn coroutine - Hunter
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEditor.Callbacks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(HealthController))]

// Resetting the individual tree chunks to their original positions is cumbersome as fuck, so here's my super awesome
// class for recording initial positions
public class PieceData{
    public GameObject obj;
    public Rigidbody rb;
    public Vector3 op;
    public Quaternion or;
    public PieceData(GameObject g_object, Vector3 original_position, Quaternion original_rotation){
        obj = g_object;
        rb = obj.GetComponent<Rigidbody>();
        op = original_position;
        or = original_rotation;
    }
}

public class TreePhysicsController : MonoBehaviour
{
    public HealthController healthController;
    //public Animator animator;

    // let the tree access the players inventory
    public int WoodValue = 10;

    // public GameObject Player;
    public WoodInventory Wallet;

    // stump to replace with when choped down
    public GameObject Stump;
    public GameObject TreeModel;

    private BoxCollider treeCollider;

    // Pivotal for implementing the break physics
    private List<PieceData> pieces;

    // Gravity's too slow. When the tree breaks, I make it a litte bit faster.
    private bool fast_falling = false;
    void Awake() {
        pieces = new List<PieceData>();

        treeCollider = GetComponent<BoxCollider>();

        // Subscribe to the OnDeath event
        healthController.OnDeath += OnDie;

        // Record EVERYTHING about the piece in its initial state. That way we can rebuild it.
        foreach(Transform child in TreeModel.transform.GetChild(0).transform){
            var obj = child.gameObject;
            var rb = obj.GetComponent<Rigidbody>();
            pieces.Add(new PieceData(obj, obj.transform.position, obj.transform.rotation));
            rb.detectCollisions = false;
            rb.useGravity = false;
        }
    }

    public void OnDie() {
        StartCoroutine(Kill());
        StartCoroutine(Respawn());

        // this is so the player cant wack the stump for infinite wood
        treeCollider.enabled = false;
    }

    IEnumerator Kill()
    {
        //animator.SetTrigger("Fall");

        // Wait for the current transition to end
        //yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f );

        // As soon as these are set to true, the tree will fall apart
        // (or explode, it really depends on if the model clips through itself...)
        foreach(PieceData piece in pieces){
            piece.rb.detectCollisions=true;
            piece.rb.useGravity=true;
        }
        Stump.SetActive(true);
        Wallet.AddWood(WoodValue);

        fast_falling = true;

        yield return new WaitForSeconds(5f);

        // Cleanup
        fast_falling = false;

        foreach(PieceData piece in pieces){
            piece.rb.detectCollisions=false;
        }

        // Wait for the death animation to end
        //yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
    }

    IEnumerator Respawn()
    {
        // This will play before it is active
        // this was giving a warning and seemed to not effect execution, commenting out and revisiting later - hunter
        //animator.SetTrigger("Stand");

        yield return new WaitForSeconds(80f);
        // for testing reasons
        //yield return new WaitForSeconds(5f);

        Stump.SetActive(false);

        // Reset everything, EVERYTHING.
        // For some reason, disabling an object doesn't reset its velocities...
        healthController.ResetHealth();
        foreach(PieceData piece in pieces){
            piece.rb.velocity = Vector3.zero;
            piece.rb.angularVelocity = Vector3.zero;
            piece.rb.detectCollisions=false;
            piece.rb.useGravity=false;
            piece.obj.transform.position = piece.op;
            piece.obj.transform.rotation = piece.or;
            piece.obj.SetActive(true);
        }
        TreeModel.SetActive(true);
        treeCollider.enabled = true;
    }
    void FixedUpdate(){
        if(fast_falling){
            foreach(PieceData piece in pieces){
                piece.rb.AddForce(Physics.gravity * 5f, ForceMode.Acceleration);
            }
        }else{
            foreach(PieceData piece in pieces){
                // May need adjustment on different maps, might redefine as public field
                if(piece.obj.transform.position.y < -10){
                    // Once a piece drops through the floor and down enough, disable it until respawn
                    piece.obj.SetActive(false);
                }
            }
        }
    }
}
