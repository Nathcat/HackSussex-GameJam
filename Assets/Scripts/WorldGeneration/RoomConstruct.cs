using System.Collections.Generic;
using UnityEngine;

public class RoomConstruct : MonoBehaviour
{
    public const float ROOM_RADUIS = 10;

    [SerializeField]
    private Dictionary<Vector2Int, Directions> connections;

    private void OnDrawGizmos()
    {
        if (Application.isPlaying) return;

        foreach (KeyValuePair<Vector2Int, Directions> paiar in connections)
        {
            if (connections.up) Gizmos.DrawIcon(transform.position + Vector3.up * ROOM_RADUIS, "connector_0.png");
            if (connections.right) Gizmos.DrawIcon(transform.position + Vector3.right * ROOM_RADUIS, "connector_90.png");
            if (connections.down) Gizmos.DrawIcon(transform.position + Vector3.down * ROOM_RADUIS, "connector_180.png");
            if (connections.left) Gizmos.DrawIcon(transform.position + Vector3.left * ROOM_RADUIS, "connector_270.png");
        }
    }

    public Dictionary<Vector2Int, Directions> GetConnections() { return connections; }
}
