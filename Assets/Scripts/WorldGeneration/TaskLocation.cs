using UnityEngine;

public class TaskLocation : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tasks;

    private Task task = null;

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "task.png");
    }

    public void SetupTask(int id)
    {
        GameObject decision = tasks[Random.Range(0, tasks.Length)];
        task = Instantiate(decision).GetComponent<Task>();
        task.id = (byte) id;

        GameObject[] netclient = GameObject.FindGameObjectsWithTag("netclient");
        if (netclient.Length != 0) {
            netclient[0].GetComponent<NetworkClient>().tasks.Add(task);
        }
    }

    public Task GetTask() { return task; }
}
