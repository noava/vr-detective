using UnityEngine;
using Meta.XR.MRUtilityKit;

public class VirtualRoomAligner : MonoBehaviour
{
    [SerializeField] private Transform virtualRoom;
    [SerializeField] private Transform alignmentReference;

    private void Awake()
    {
        MRUK.Instance.RegisterSceneLoadedCallback(AlignRoom);
    }

    private void AlignRoom()
    {
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();

        if (room == null)
        {
            Debug.LogError("No MRUK room found.");
            return;
        }

        MRUKAnchor keyWall = room.GetKeyWall(out Vector2 wallScale);

        if (keyWall == null)
        {
            Debug.LogError("MRUK could not find a key wall.");
            return;
        }

        Transform physicalWall = keyWall.transform;

        Quaternion targetRotation =
            physicalWall.rotation *
            Quaternion.Inverse(alignmentReference.localRotation);

        Vector3 targetPosition =
            physicalWall.position -
            targetRotation * alignmentReference.localPosition;

        virtualRoom.SetPositionAndRotation(
            targetPosition,
            targetRotation
        );

        Debug.Log("Virtual room aligned to MRUK key wall.");
    }
}
