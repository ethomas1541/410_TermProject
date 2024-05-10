using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementMessage : MonoBehaviour
{
    public MessageController MSGController;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {   
            MSGController.ChangeMessage();
            // trigger is only used once, can be disposed once it it tripped
            Destroy(gameObject);
        }
    }

}
