using UnityEngine;
using UnityEngine.UI;

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
    }
}
