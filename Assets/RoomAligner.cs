using UnityEngine;
using Meta.XR.MRUtilityKit;

public class RoomAligner : MonoBehaviour
{
    public Transform virtualReference;

    private void Start()
    {
        MRUK.Instance.SceneLoadedEvent.AddListener(AlignRoom);
    }

    private void AlignRoom()
    {
        var room = MRUK.Instance.GetCurrentRoom();

        if (room == null)
        {
            Debug.LogError("No MRUK room found.");
            return;
        }

        // Get the live MRUK wall anchors
        var walls = room.WallAnchors;

        if (walls.Count == 0)
        {
            Debug.LogError("No MRUK walls found.");
            return;
        }

        // TEMPORARY: use the first wall
        Transform realWall = walls[0].transform;

        // Match rotation
        transform.rotation = realWall.rotation;

        // Move the entire virtual room so
        // VirtualReference sits on the MRUK wall.
        Vector3 difference =
            virtualReference.position - transform.position;

        transform.position =
            realWall.position - difference;

        Debug.Log("Virtual room aligned.");
    }
}