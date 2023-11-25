using System.Collections.Generic;
using UnityEngine;

public class RoomConnector : MonoBehaviour
{
    [SerializeField]
    private ConnectorDirection Direction;

    [SerializeField]
    private int id;

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, Direction switch
        {
            ConnectorDirection.PositiveY => "connector_0.png",
            ConnectorDirection.PositiveX => "connector_90.png",
            ConnectorDirection.NegativeY => "connector_180.png",
            ConnectorDirection.NegativeX => "connector_270.png",
            _ => throw new System.NotImplementedException(),
        });
    }

    public int GetId()
    {
        return id;
    }

    public void DestroyOn(GameObject obj)
    {
        RoomConnector[] connectors = obj.GetComponentsInChildren<RoomConnector>();
        foreach (RoomConnector connector in connectors)
        {
            if (connector.GetId() == id)
            {
                Destroy(connector.gameObject);
                return;
            }
        }

        throw new KeyNotFoundException();
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public ConnectorDirection GetDirection() { 
        return Direction;
    }

    public GameObject GetRoom()
    {
        return transform.parent.gameObject;
    }
}
