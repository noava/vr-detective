using UnityEngine;
using Meta.XR.MRUtilityKit;

public class AlignVirtualRoomToBed : MonoBehaviour
{
    public Transform virtualBed;

    private void OnEnable()
    {
        MRUK.Instance.SceneLoadedEvent.AddListener(OnRoomLoaded);
    }

    private void OnDisable()
    {
        if (MRUK.Instance != null)
            MRUK.Instance.SceneLoadedEvent.RemoveListener(OnRoomLoaded);
    }

    private void OnRoomLoaded()
    {
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();

        if (room == null)
        {
            Debug.LogError("No MRUK room found.");
            return;
        }

        MRUKAnchor realBed = null;

        foreach (MRUKAnchor anchor in room.Anchors)
        {
            if (anchor.HasAnyLabel(MRUKAnchor.SceneLabels.BED))
            {
                realBed = anchor;
                break;
            }
        }

        if (realBed == null)
        {
            Debug.LogError("No BED was found in the Quest room data.");
            return;
        }

        if (virtualBed == null)
        {
            Debug.LogError("Virtual Bed has not been assigned.");
            return;
        }

        AlignRoom(realBed.transform);

        Debug.Log("Virtual room aligned to real BED.");
    }

    private void AlignRoom(Transform realBed)
    {
        Transform roomRoot = transform;

        Vector3 bedLocalPosition =
            roomRoot.InverseTransformPoint(virtualBed.position);

        Quaternion bedLocalRotation =
            Quaternion.Inverse(roomRoot.rotation) *
            virtualBed.rotation;

        roomRoot.rotation =
            realBed.rotation *
            Quaternion.Inverse(bedLocalRotation);

        roomRoot.position =
            realBed.position -
            roomRoot.rotation * bedLocalPosition;
    }
}