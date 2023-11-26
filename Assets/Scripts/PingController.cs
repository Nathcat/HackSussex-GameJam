using UnityEngine;

public class PingController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] playerPings = new GameObject[0];

    private NetworkServer server;

    void Start()
    {
        server = FindFirstObjectByType<NetworkServer>();
    }

    void Update()
    {
        for (int i = 0; i < 6; i++)
        {
            Vector3 position = server.playerPositions[i];
            if (position == null) continue;
            playerPings[i].transform.position = Camera.main.WorldToScreenPoint(position);
        }
    }
}
