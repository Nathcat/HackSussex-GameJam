using Unity.VisualScripting;
using UnityEngine;

public class PingController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] playerPings = new GameObject[0];

    [SerializeField]
    private GameObject doorPrefab;

    [SerializeField]
    private GameObject taskPrefab;

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

    public void PostWorldGen()
    {
        foreach (doorScript door in FindObjectsByType<doorScript>(FindObjectsSortMode.None))
        {
            GameObject button = Instantiate(doorPrefab, transform);
            button.GetComponent<DoorButtonController>().Door = door;
        }
    
        foreach (TaskLocation task in FindObjectsByType<TaskLocation>(FindObjectsSortMode.None))
        {
            if (task.GetTask() == null) continue;
            GameObject ping = Instantiate(taskPrefab, transform);
            ping.GetComponent<TaskPing>().task = task.gameObject;
        }
    }
}
