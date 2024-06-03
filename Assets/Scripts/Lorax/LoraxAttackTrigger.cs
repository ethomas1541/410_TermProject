using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class LoraxAttackTrigger : MonoBehaviour
{
    public Lorax lorax;

    void Start()
    {
        lorax = GetComponentInParent<Lorax>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            lorax.isShooting = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            lorax.isApproaching = true;
        }
    }
}
