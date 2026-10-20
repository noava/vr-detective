using UnityEngine;

public class TurnOnGravityOnGrab : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Keep the painting suspended initially.
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    // Call this when the painting is grabbed.
    public void OnGrabbed()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }
}
