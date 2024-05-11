using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTrigger : MonoBehaviour
{
    public List<GameObject> enables;
    public List<string> activatedByTags;

    void OnTriggerEnter(Collider other) {

        // This object can trigger this event
        if (activatedByTags.Contains(other.tag)) {

            // Enable each enables object
            foreach (GameObject t in enables) {
                t.SetActive(true);
            }
        }
        // script should trigger once
        Destroy(gameObject);
    }
}
