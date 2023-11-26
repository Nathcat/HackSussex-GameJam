using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField]
    private int WorldSize = 16;

    [SerializeField]
    private GameObject SpawnRoom;

    [SerializeField]
    private GameObject[] Rooms;

    private RoomConstruct[] RoomConstructs;

    Directions[,] ConnectionsMap;
    bool[,] ExistenceMap;

    private void Start()
    {
        Generate(42);
    }

    public void Generate(int seed)
    {
        // Cache the room constructs of room prefabs
        RoomConstructs = new RoomConstruct[Rooms.Length];
        for (int i = 0; i < Rooms.Length; i++) RoomConstructs[i] = Rooms[i].GetComponent<RoomConstruct>();

        ConnectionsMap = new Directions[WorldSize, WorldSize];
        ExistenceMap = new bool[WorldSize, WorldSize];
        Random.InitState(seed);

        // Begin facing up
        int half = WorldSize / 2;
        RoomConstruct spawnConstruct = SpawnRoom.GetComponent<RoomConstruct>();
        GenerateRoomAt(spawnConstruct, half, half);
    }

    private void GenerateRandomRoomAt(int x, int y)
    {
        // Pick room
        RoomConstruct[] possibilities = ComputePossibleRooms(x, y);
        RoomConstruct decision = possibilities[Random.Range(0, possibilities.Length)];

        GenerateRoomAt(decision, x, y);
    }

    private void GenerateRoomAt(RoomConstruct room, int x, int y)
    {
        // Instantiate
        Vector2 position = new(x * RoomConstruct.ROOM_RADIUS * 2, y * RoomConstruct.ROOM_RADIUS * 2);
        position -= new Vector2(RoomConstruct.ROOM_RADIUS * WorldSize, RoomConstruct.ROOM_RADIUS * WorldSize);
        Instantiate(room.gameObject, position, Quaternion.identity, transform);

        // Write to array
        foreach (DirectionsOffsetPair pair in room.GetConnections())
        {
            int rX = x + pair.offset.x;
            int rY = y + pair.offset.y;

            if (rX < 0 || rY < 0 || rX >= WorldSize || rY >= WorldSize) continue;

            ConnectionsMap[rX, rY] = pair.directions;
            ExistenceMap[rX, rY] = true;

            // Generate Connections
            GenerateConnectionsAt(rX, rY);
        }
    }

    private void GenerateConnectionsAt(int x, int y)
    {
        Directions current = ConnectionsMap[x, y];
        if (current.up && !ExistsAt(x, y + 1)) GenerateRandomRoomAt(x, y + 1);
        if (current.right && !ExistsAt(x + 1, y)) GenerateRandomRoomAt(x + 1, y);
        if (current.down && !ExistsAt(x, y - 1)) GenerateRandomRoomAt(x, y - 1);
        if (current.left && !ExistsAt(x - 1, y)) GenerateRandomRoomAt(x - 1, y);
    }

    public RoomConstruct[] ComputePossibleRooms(int x, int y)
    {
        List<RoomConstruct> result = new();
        foreach (RoomConstruct room in RoomConstructs)
        {
            if (CanPlaceRoomAt(room, x, y)) result.Add(room);
        }

        return result.ToArray();
    }

    public bool CanPlaceRoomAt(RoomConstruct room, int x, int y)
    {
        foreach (DirectionsOffsetPair pair in room.GetConnections())
        {
            Directions conns = pair.directions;
            int rX = x + pair.offset.x;
            int rY = y + pair.offset.y;

            if (ExistsAt(rX, rY + 1) && GetConnectionsAt(rX, rY + 1).down != conns.up) return false;
            if (ExistsAt(rX + 1, rY) && GetConnectionsAt(rX + 1, rY).left != conns.right) return false;
            if (ExistsAt(rX, rY - 1) && GetConnectionsAt(rX, rY - 1).up != conns.down) return false;
            if (ExistsAt(rX - 1, rY) && GetConnectionsAt(rX - 1, rY).right != conns.left) return false;
        }
        return true;
    }

    public Directions GetConnectionsAt(int x, int y) {
        if (x < 0 || y < 0 || x >= WorldSize || y >= WorldSize) return new Directions();
        return ConnectionsMap[x, y];
    }

    public bool ExistsAt(int x, int y)
    {
        if (x < 0 || y < 0 || x >= WorldSize || y >= WorldSize) return true;
        return ExistenceMap[x, y];
    }
}
