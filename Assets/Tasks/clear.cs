using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class clear : Task
{
    // Start is called before the first frame update
    public GameObject eventsystem;
    public GameObject host;
    public void clicking() {

        eventsystem.GetComponent<task_manager>().tasks += 1;
        complete = true;
        host.gameObject.SetActive(false);
    }
    private void load()
    {
        host.gameObject.SetActive(false);
    }
}
