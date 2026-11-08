//https://communityforums.atmeta.com/unity-development-123/figuring-out-how-to-create-a-hand-menu-palm-hud-143414?postid=1047965#post1047965
using UnityEngine;

namespace jdp{
public class HandUI_PalmActivation : MonoBehaviour
{

    // Reference to the wrist bone of the tracked VR hand.
    public Transform wristBoneTransform;

    // The GameObject to activate/deactivate.
    public GameObject objectToActivate;

    // The angle threshold to determine if the palm is facing towards the player.
    public float activationAngleThreshold = 45.0f;

    private void Start() {
        objectToActivate.SetActive(false);
    }

    void Update()
    {
        // Ensure we have references set
        if (wristBoneTransform == null || objectToActivate == null)
            return;

        // The direction from the wrist bone to the camera/HMD.
        Vector3 directionToCamera = (Camera.main.transform.position - wristBoneTransform.position).normalized;

        // The local up vector of the wrist bone when the palm is facing up.
        // Adjust this if your VR hand's palm up orientation is different.
        Vector3 palmNormal = -wristBoneTransform.up;

        // Calculate the angle between the palm's normal vector and the direction to the camera.
        float angleToCamera = Vector3.Angle(palmNormal, directionToCamera);

        // Activate the GameObject if the palm is facing the camera within the threshold.
        if (angleToCamera <= activationAngleThreshold)
        {
            objectToActivate.SetActive(true);
        }
        else
        {
            objectToActivate.SetActive(false);
        }
    }
}
}