using UnityEngine;
using UnityEngine.UI;
using System;

public class DoorButtonController : MonoBehaviour
{
    [HideInInspector]
    public doorScript Door;
    private bool Open = true;

    [SerializeField]
    private Sprite closeSprite;

    [SerializeField]
    private Sprite openSprite;

    private Image imageRenderer;

    private void Start()
    {
        imageRenderer = GetComponent<Image>();
    }

    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(Door.transform.position);
    }

    public void click()
    {
        Open = !Open;
        if (Open)
        {
            Door.id = (byte)(Door.id | 0x80);
            imageRenderer.sprite = openSprite;
        }
        else
        {
            Door.id = (byte)(Door.id & 0x7F);
            imageRenderer.sprite = closeSprite;
        }

        WorldGenerator worldGen = FindFirstObjectByType<WorldGenerator>();
        byte[] door_packet = new byte[worldGen.doors.Length + 5];
        door_packet[0] = NetworkServer.PACKETTYPE_DOORUPDATE;
        byte[] length_buffer = BitConverter.GetBytes(worldGen.doors.Length);
        Buffer.BlockCopy(length_buffer, 0, door_packet, 1, 4);
        for (int i = 0; i < worldGen.doors.Length; i++) {
            door_packet[i+5] = worldGen.doors[i].id;
        }

        GameObject.Find("GameManager").GetComponent<NetworkServer>().ft.door_packet = door_packet;
    }
}
