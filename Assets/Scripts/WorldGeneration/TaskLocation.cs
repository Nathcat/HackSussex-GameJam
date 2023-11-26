using UnityEngine;

public class TaskLocation : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tasks;

    private Task task;

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "task.png");
    }

    public void SetupTask(int id)
    {
        task = tasks[Random.Range(0, tasks.Length)].GetComponent<Task>();
        task.id = (byte) id;
    }

    public Task GetTask() { return task; }
}
