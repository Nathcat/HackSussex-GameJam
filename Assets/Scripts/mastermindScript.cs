using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using System.Random;

public class mastermindScript : MonoBehaviour
{
    public TMP_Text digit1;
    public TMP_Text digit2;
    public TMP_Text digit3;
    public TMP_Text digit4;
    
    private int ones;
    private int zeros;
    private int num1;
    private int num2;
    private int num3;
    private int num4;
    public int code;

    //private random rnd = new random();

    

    
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        code = Random.Range(0, 16);
        num1 = code / 8;
        num2 = (code % 8) / 4;
        num3 = (code % 4) / 2;
        num4 = (code % 2) / 1;

        if(num1 == 1)
        {
            ones++;
        }
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void swap1()
    {
        
        if(digit1.text == "0")
        {
            digit1.text = "1";
        }
        else
        {
            digit1.text = "0";
        }
    }

    public void swap2()
    {
        
        if(digit2.text == "0")
        {
            digit2.text = "1";
        }
        else
        {
            digit2.text = "0";
        }
    }

    public void swap3()
    {
        
        if(digit3.text == "0")
        {
            digit3.text = "1";
        }
        else
        {
            digit3.text = "0";
        }
    }

    public void swap4()
    {
        
        if(digit4.text == "0")
        {
            digit4.text = "1";
        }
        else
        {
            digit4.text = "0";
        }
    }


    public void enter()
    {

    }
    
}
