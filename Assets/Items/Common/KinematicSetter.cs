using UnityEngine;

public class KinematicSetter : MonoBehaviour
{
    public Rigidbody rb;

    public void DisableKinematic()
    {
        rb.isKinematic = false;
    }
}
