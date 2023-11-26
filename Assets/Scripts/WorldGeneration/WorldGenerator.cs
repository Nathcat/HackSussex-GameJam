using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField]
    private int WorldSize = 16;

    [SerializeField]
    private GameObject[] rooms;

    private RoomConstruct[] roomConstructs;

    Directions[,] connectionsMap;
    bool[,] existenceMap;

    void Start()
    {
        // Cache the room constructs of room prefabs
        roomConstructs = new RoomConstruct[rooms.Length];
        for (int i = 0; i < rooms.Length; i++) roomConstructs[i] = rooms[i].GetComponent<RoomConstruct>();
    }

    public void Generate(int seed)
    {
        connectionsMap = new Directions[WorldSize, WorldSize];
        existenceMap = new bool[WorldSize, WorldSize];
        Random.InitState(seed);

        // Begin facing up
        GenerateRoomAt(8, 8);
    }

    private void GenerateConnectionsAt(int x, int y)
    {
        Directions current = connectionsMap[x, y];
        if (current.up && !ExistsAt(x, y + 1)) GenerateRoomAt(x, y + 1);
        if (current.right && !ExistsAt(x + 1, y)) GenerateRoomAt(x + 1, y);
        if (current.down && !ExistsAt(x, y - 1)) GenerateRoomAt(x, y - 1);
        if (current.left && !ExistsAt(x - 1, y)) GenerateRoomAt(x - 1, y);
    }

    private void GenerateRoomAt(int x, int y)
    {
        // Pick room
        RoomConstruct[] possibilities = ComputePossibleRooms(x, y);
        if (possibilities.Length == 0) return;

        RoomConstruct decision = possibilities[Random.Range(0, possibilities.Length)];

        // Instantiate
        Vector2 position = new(x * RoomConstruct.ROOM_RADUIS * 2, y * RoomConstruct.ROOM_RADUIS * 2);
        Instantiate(decision.gameObject, position, Quaternion.identity, transform);

        // Write to array
        connectionsMap[x, y] = decision.GetConnections();
        existenceMap[x, y] = true;

        // Generate Connections
        GenerateConnectionsAt(x, y);
    }

    public RoomConstruct[] ComputePossibleRooms(int x, int y)
    {
        List<RoomConstruct> result = new();
        foreach (RoomConstruct room in roomConstructs)
        {
            Directions connections = room.GetConnections();
            if (ExistsAt(x, y + 1) && GetConnectionsAt(x, y + 1).down != connections.up) continue;
            if (ExistsAt(x + 1, y) && GetConnectionsAt(x + 1, y).left != connections.right) continue;
            if (ExistsAt(x, y - 1) && GetConnectionsAt(x, y - 1).up != connections.down) continue;
            if (ExistsAt(x - 1, y) && GetConnectionsAt(x - 1, y).right != connections.left) continue;
            result.Add(room);
        }

        return result.ToArray();
    }

    public Directions GetConnectionsAt(int x, int y) {
        if (x < 0 || y < 0 || x >= WorldSize || y >= WorldSize) return new Directions();
        return connectionsMap[x, y];
    }

    public bool ExistsAt(int x, int y)
    {
        if (x < 0 || y < 0 || x >= WorldSize || y >= WorldSize) return true;
        return existenceMap[x, y];
    }
}
