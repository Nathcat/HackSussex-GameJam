using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class slide_task : Task
{
    public TMP_Text textbox;
    public Slider slide;
    public RectTransform panel;
    public bool IsMoving;
    public bool up;
    public float speed;
    [SerializeField] private float x = 0;
    public bool pressed = false;
    [SerializeField] private float y;
    public GameObject eventsystem;
    public GameObject host;
    
    // Start is called before the first frame update
    public void load()
    {

        textbox.text = "Press the button";
    }

    public void press() 
    {
        if (pressed)
        {
            y = slide.value * 750;
            if (y > 375) {
                y = y - 375;
            }
            else if (y < 375) {
                y = y- 375;
            }

                if (y >= x - 18 && y <= x + 18)
                {
                    IsMoving = false;
                    //eventsystem.GetComponent<task_manager>().tasks += 1;
                    complete = true;
                    host.gameObject.SetActive(false);
                }
                else
                {
                    textbox.text = "try again";
                    x = UnityEngine.Random.Range(-375, 375);
                    panel.localPosition = new Vector3(x, -52, 0);
                    IsMoving = true;
                    pressed = true;
                    //task fail
                }



        }
        else
        {
            textbox.text = "press button again when the redfff circle is in the green area";
            x = UnityEngine.Random.Range(-375, 375);
            panel.localPosition = new Vector3(x, -52, 0);
            IsMoving = true;
            pressed = true;
        }
    }

    private void Update()
    {
        if (IsMoving == true)
        {
            if (up == true)
            {
                slide.value += (0.1f * speed * Time.deltaTime);
            }
            else
            {
                slide.value -= (0.1f * speed * Time.deltaTime);

            }
            if (slide.value == 1)
            {
                up = false;
            }
            else if (slide.value == 0)
            {
                up = true;
            }

        }
    }

}
