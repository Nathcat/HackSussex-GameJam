using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonController : MonoBehaviour
{
    [HideInInspector]
    public doorScript Door;
    private bool Open = true;

    [SerializeField]
    private Sprite closeSprite;

    [SerializeField]
    private Sprite openSprite;

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
            GetComponent<SpriteRenderer>().sprite = closeSprite;
        }
        else
        {
            Door.id = (byte)(Door.id & 0x7F);
            GetComponent<SpriteRenderer>().sprite = openSprite;
        }
    }
}
