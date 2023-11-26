using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class tyler_says : MonoBehaviour
{
    private List<int> colours = new List<int> ();
    public Button blue;
    public Button green;
    public Button red;
    public Button yellow;
    public Button purple;
    public int current;
    public int index = 0;
    public GameObject eventsystem;
    public GameObject host;
    private void Start()
    {
        host.gameObject.SetActive(false);
    }
    public void next() {
        int color = Random.Range(1, 5);
        colours.Add(color);
        StartCoroutine(flash());
    
    }
    public IEnumerator flash() {
        for (int i = 0; i < colours.Count; i++)
        {
            yield return new WaitForSeconds(0.5f);
            current = colours[i];
            if (current == 0)
            {
                red.image.color = Color.red;

            }
            else if (current == 1)
            {
                yellow.image.color = Color.yellow;

            }
            else if (current == 2)
            {

                purple.image.color = new Color(255, 0, 255);
            }
            else if (current == 3)
            {
                green.image.color = Color.green;

            }
            else if (current == 4) { 
                blue .image.color = Color.blue;
            }

            yield return new WaitForSeconds(0.5f);

            red.image.color = Color.white;
            blue .image.color = Color.white;
            yellow .image.color = Color.white;
            purple .image.color = Color.white;
            green .image.color = Color.white;
        }
    }

    public void redbutton() {
        if (colours[index] == 0)
        {
            index++;
            if (index >= 4)
            {
                eventsystem.GetComponent<task_manager>().tasks += 1;
                host.gameObject.SetActive(false);
                colours.Clear();
            }
            if (index == colours.Count) 
            {
                index = colours.Count;
                index = 0;
                next();
            }
        }
        else {
            index = 0;
            colours.Clear();
            next();
        }
    }
    public void yellowbutton()
    {
        if (colours[index] == 1)
        {
            if (index >= 4)
            {
                eventsystem.GetComponent<task_manager>().tasks += 1;
                host.gameObject.SetActive(false);
                colours.Clear();
            }
            index++;
            if (index == colours.Count)
            {
                index = colours.Count;
                index = 0;
                next();
            }

        }
        else
        {
            index = 0;
            colours.Clear();
            next();
        }
    }
    public void purplebutton()
    {
        if (colours[index] == 2)
        {
            if (index >= 4)
            {
                eventsystem.GetComponent<task_manager>().tasks += 1;
                host.gameObject.SetActive(false);
                colours.Clear();
            }
            index++;
            if (index == colours.Count)
            {
                index = colours.Count;
                index = 0;
                next();
            }

        }
        else
        {
            index = 0;
            colours.Clear();
            next();
        }
    }
    public void greenbutton()
    {
        if (colours[index] == 3)
        {
            if (index >= 4)
            {
                eventsystem.GetComponent<task_manager>().tasks += 1;
                host.gameObject.SetActive(false);
                colours.Clear();
            }
            index++;
            if (index == colours.Count)
            {
                index = colours.Count;
                index = 0;
                next();
            }

        }
        else
        {
            index = 0;
            colours.Clear();
            next();
            
        }
    }
    public void bluebutton()
    {
        if (colours[index] == 4)
        {
            if (index >= 4)
            {
                eventsystem.GetComponent<task_manager>().tasks += 1;
                host.gameObject.SetActive(false);
                colours.Clear();
            }
            index++;
            if (index == colours.Count)
            {
                index = colours.Count;
                index = 0;
                next();
                
            }

        }
        else
        {
            index = 0;
            colours.Clear();
            next();
        }
    }


}
