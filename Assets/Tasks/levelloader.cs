using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelloader : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fishs;
    public GameObject slider;
    public GameObject wires;
    public GameObject dig;
    public GameObject simon;
    public GameObject master;
    void Start()
    {
       fishs.gameObject.SetActive(false);
        slider.gameObject.SetActive(false);
        wires.gameObject.SetActive(false);
        dig.gameObject.SetActive(false);
        simon.gameObject.SetActive(false);
        master.gameObject.SetActive(false);

    }

    // Update is called once per frame
    public void fish() { 
        fishs.SetActive(true);
        fishs.GetComponent<fishing>().load();
    }
    public void sliders()
    {
        slider.SetActive(true);
        slider.GetComponent<slide_task>().load();
    }
    public void wire()
    {
        wires.SetActive(true);
        wires.GetComponent<wires>().load();
    }
    public void digs()
    {
        dig.SetActive(true);
    }
    public void semone()
    {
        simon.SetActive(true);
        
    }
    public void masters() { 
        master.SetActive(true);
        master.GetComponent<mastermindScript>().load();
    }
}
