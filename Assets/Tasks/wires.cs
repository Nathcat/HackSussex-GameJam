using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class wires : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject redwire;
    public GameObject bluewire;
    public GameObject yellowwire;
    public GameObject greenwire;
    public int welded=0;
    public GameObject eventsystem;
    public GameObject host;
    public string current = "";
    public void load()
    {
        redwire.gameObject.SetActive(false);
        bluewire.gameObject.SetActive(false);
        greenwire.gameObject.SetActive(false);
        yellowwire.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void startblue() {
        current = "blue";
    }
    public void startgreen() { 
        current = "green";
    }
    public void startred()
    {
        current = "red";
    }
    public void startyellow()
    {
        current = "yellow";
    }
    public void endblue()
    {
        if (current == "blue") { 
            bluewire.gameObject.SetActive(true);
            welded++;
        }
        
        if (welded == 4)
        {
            eventsystem.GetComponent<task_manager>().tasks += 1;
            host.gameObject.SetActive(false);
        }
    }
    public void endgreen()
    {
        if (current == "green")
        {
            greenwire.gameObject.SetActive(true);
            welded++;
        }

        if (welded == 4)
        {
            eventsystem.GetComponent<task_manager>().tasks += 1;
            host.gameObject.SetActive(false);
        }
    }
    public void endred()
    {
        if (current == "red")
        {
            redwire.gameObject.SetActive(true);
            welded++;
        }

        if (welded == 4)
        {
            eventsystem.GetComponent<task_manager>().tasks += 1;
            host.gameObject.SetActive(false);
        }
    }
    public void endyellow()
    {
        if (current == "yellow")
        {
            yellowwire.gameObject.SetActive(true);
            welded++;
        }

        if (welded == 4)
        {
            eventsystem.GetComponent<task_manager>().tasks += 1;
            host.gameObject.SetActive(false);
        }
    }

}
