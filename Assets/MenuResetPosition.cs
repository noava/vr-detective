using UnityEngine;

public class MenuTrackerReset : MonoBehaviour
{
    void Start()
    {
        ResetTracking();
    }

    public void ResetTracking()
    {
        // 1. Tving Meta Quest-hodesettet til å nullstille hodesporingen (Recenter)
        // Dette gjør det samme som å holde nede Oculus/Meta-knappen på kontrolleren.
        if (OVRManager.display != null)
        {
            OVRManager.display.RecenterPose();
        }

        // 2. Flytt i tillegg hele OVRCameraRig tilbake til startkoordinatene som en ekstra sikkerhet
        GameObject rigObj = GameObject.Find("OVRCameraRig");
        if (rigObj != null)
        {
            rigObj.transform.position = new Vector3(0f, 0.8f, 0f);
            rigObj.transform.rotation = Quaternion.identity; // Setter rotasjon til 0
        }

        Debug.Log("VR Tracking og Rig ble vellykket nullstilt til (0, 0.8, 0)!");
    }
}