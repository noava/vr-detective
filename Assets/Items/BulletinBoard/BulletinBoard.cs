using UnityEngine;

public class BulletinBoard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        
        if (rb != null)
        {
            rb.useGravity = false;
            
            rb.linearDamping = 100f;
            rb.angularDamping = 100f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        if (rb != null)
        {
            rb.useGravity = true;
            
            // If we need to save damping values we need to work on this script abit more. 
            // For now this is setting to a default value we already know works.
            rb.linearDamping = 0f;
            rb.angularDamping = 0.05f;
        }
    }
}
