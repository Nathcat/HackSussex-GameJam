using UnityEngine;

public class TaskPing : MonoBehaviour
{
    [HideInInspector]
    public GameObject task;

    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(task.transform.position);
    }
}
