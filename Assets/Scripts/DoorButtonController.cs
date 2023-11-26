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

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            spriteRenderer.sprite = closeSprite;
        }
        else
        {
            Door.id = (byte)(Door.id & 0x7F);
            spriteRenderer.sprite = openSprite;
        }
    }
}
