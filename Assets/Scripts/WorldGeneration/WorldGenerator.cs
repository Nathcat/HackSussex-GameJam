using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] rooms;

    void Start()
    {
        Generate(42);
    }

    public void Generate(int seed)
    {
        // Reset the randomiser
        Random.InitState(seed);

        // Generate connections bassed off this gameobject
        GenerateConnections(gameObject, 1);
    }

    public void GenerateConnections(GameObject room, int distance)
    {
        RoomConnector[] connectors = room.GetComponentsInChildren<RoomConnector>();
        foreach (RoomConnector connector in connectors) {
            // Pick room
            RoomConnector[] possible = GetPossibleConnections(connector);
            RoomConnector decision = possible[Random.Range(0, possible.Length)];
            
            // Instantiate
            Vector2 position = connector.GetPosition() - decision.GetPosition();
            GameObject child = Instantiate(decision.GetRoom(), position, Quaternion.identity, transform);

            // Clean up connectors
            Destroy(connector.gameObject);
            decision.DestroyOn(child);

            // Recurse
            if (distance > 0) GenerateConnections(child, distance - 1);
        }
    }

    private RoomConnector[] GetPossibleConnections(RoomConnector connector)
    {
        ConnectorDirection direction = connector.GetDirection().Inverse();

        List<RoomConnector> possibleConnectors = new();
        foreach (GameObject room in rooms)
        {
            RoomConnector[] connectors = room.GetComponentsInChildren<RoomConnector>();
            foreach (RoomConnector possible in connectors)
            {
                // Ensure the connector is facing the right way
                if (possible.GetDirection() == direction)
                {
                    possibleConnectors.Add(possible);
                }
            }
        }

        return possibleConnectors.ToArray();
    }
}
