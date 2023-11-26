using System.Collections.Generic;
using UnityEngine;

public class RoomConstruct : MonoBehaviour
{
    public const float ROOM_RADIUS = 10;

    [SerializeField]
    private DirectionsOffsetPair[] connections = new DirectionsOffsetPair[0];

    private void OnDrawGizmos()
    {
        if (Application.isPlaying) return;

        foreach (DirectionsOffsetPair pair in connections)
        {
            Vector3 offset = new Vector3(pair.offset.x * ROOM_RADIUS, pair.offset.y * ROOM_RADIUS) * 2;

            if (pair.directions.up) Gizmos.DrawIcon(transform.position + Vector3.up * ROOM_RADIUS + offset, "connector_0.png");
            if (pair.directions.right) Gizmos.DrawIcon(transform.position + Vector3.right * ROOM_RADIUS + offset, "connector_90.png");
            if (pair.directions.down) Gizmos.DrawIcon(transform.position + Vector3.down * ROOM_RADIUS + offset, "connector_180.png");
            if (pair.directions.left) Gizmos.DrawIcon(transform.position + Vector3.left * ROOM_RADIUS + offset, "connector_270.png");
        }
    }

    public DirectionsOffsetPair[] GetConnections() { return connections; }
}
