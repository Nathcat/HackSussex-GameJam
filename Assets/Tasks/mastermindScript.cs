using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using System.Random;

public class mastermindScript : Task
{
    public TMP_Text digit1;
    public TMP_Text digit2;
    public TMP_Text digit3;
    public TMP_Text digit4;

    public Image ind1;
    public Image ind2;
    public Image ind3;
    public Image ind4;

    public GameObject eventsystem;
    public GameObject host;
    public int wins;

    private int ones = 0;
    private int zeros = 0;
    private int num1;
    private int num2;
    private int num3;
    private int num4;
    public int code;
    //private int inputOnes;
    //private int inputZeros;

    private bool maybe1;
    private bool maybe2;
    private bool maybe3;
    private bool maybe4;



    //private random rnd = new random();

    

    
    
    // Start is called before the first frame update
    public override void load()
    {
        //gameObject.SetActive(false);
        code = Random.Range(0, 16);
        
        num1 = code / 8;
        num2 = (code % 8) / 4;
        num3 = (code % 4) / 2;
        num4 = (code % 2) / 1;

        
        


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
        ones = 0;
        zeros= 0;

        maybe1 = false;
        maybe2 = false;
        maybe3 = false;
        maybe4 = false;



        if(num1 == 1)
        {
            ones++;
        }
        else
        {
            zeros++;
        }
        if(num2 == 1)
        {
            ones++;
        }
        else
        {
            zeros++;
        }
        if(num3 == 1)
        {
            ones++;
        }
        else
        {
            zeros++;
        }
        if(num4 == 1)
        {
            ones++;
        }
        else
        {
            zeros++;
        }
        if (digit1.text == (code / 8).ToString() && digit2.text == ((code % 8) / 4).ToString() && digit3.text == ((code % 4) / 4).ToString() && digit4.text == ((code % 2) / 4).ToString())
        {
            //eventsystem.GetComponent<task_manager>().tasks += 1;
            complete = true;
            host.gameObject.SetActive(false);
            code = Random.Range(0, 16);

        }

        if (digit1.text == (code / 8).ToString())
        {
            ind1.color = new Color32(11,204,40,100);
            Debug.Log("w");
            if(digit1.text == "0")
            {
                zeros--;
            }
            else
            {
                ones--;
            }
        }
        else if(digit1.text == "1" && ones > 0 || digit1.text == "0" && zeros > 0)
        {
            maybe1 = true;
        }
        else
        {
            ind1.color = new Color32(248,1,1,100);
        }
        if(digit2.text == ((code % 8) / 4).ToString())
        {
            ind2.color = new Color32(11,204,40,100);
            Debug.Log("w");
            if(digit2.text == "0")
            {
                zeros--;
            }
            else
            {
                ones--;
            }
        }
        else if(digit2.text == "1" && ones > 0 || digit2.text == "0" && zeros > 0)
        {
            maybe2 = true;
        }
        else
        {
            ind2.color = new Color32(248,1,1,100);
        }
        if(digit3.text == ((code % 4) / 2).ToString())
        {
            ind3.color = new Color32(11,204,40,100);
            Debug.Log("w");
            if(digit3.text == "0")
            {
                zeros--;
            }
            else
            {
                ones--;
            }
        }
        else if(digit3.text =="1" && ones > 0 || digit3.text == "0" && zeros > 0)
        {
            maybe3 = true;
        }
        else
        {
            ind3.color = new Color32(248,1,1,100);
        }
        if(digit4.text == ((code % 2) / 1).ToString())
        {
            ind4.color = new Color32(11,204,40,100);
            if(digit4.text == "0")
            {
                zeros--;
            }
            else
            {
                ones--;
            }
        }
        else if(digit4.text == "1" && ones > 0 || digit4.text == "0" && zeros > 0)
        {
            maybe4 = true;
        }
        else
        {
            ind4.color = new Color32(248,1,1,100);
        }

        






        if(maybe1)
        {
            if(digit1.text == "1" && ones > 0)
            {
                ind1.color = new Color32(255,97,0,100);
                ones--;
            }
            else if(digit1.text == "0" && zeros > 0)
            {
                ind1.color = new Color32(255,97,0,100);
                zeros--;
            }
            else
            {
                ind1.color = new Color32(248,1,1,100);
            }
        }
        if(maybe2)
        {
            if(digit2.text == "1" && ones > 0)
            {
                ind2.color = new Color32(255,97,0,100);
                ones--;
            }
            else if(digit2.text == "0" && zeros > 0)
            {
                ind2.color = new Color32(255,97,0,100);
                zeros--;
            }
            else
            {
                ind2.color = new Color32(248,1,1,100);
            }
        }
        if(maybe3)
        {
            if(digit3.text == "1" && ones > 0)
            {
                ind3.color = new Color32(255,97,0,100);
                ones--;
            }
            else if(digit3.text == "0" && zeros > 0)
            {
                ind3.color = new Color32(255,97,0,100);
                zeros--;
            }
            else
            {
                ind3.color = new Color32(248,1,1,100);
            }
        }
        if(maybe4)
        {
            if(digit4.text == "1" && ones > 0)
            {
                ind4.color = new Color32(255,97,0,100);
                ones--;
            }
            else if(digit4.text == "0" && zeros > 0)
            {
                ind4.color = new Color32(255,97,0,100);
                zeros--;
            }
            else
            {
                ind4.color = new Color32(248,1,1,100);
            }
        }
        






        /*switch(zeros)
        {
            case -1:
                if(maybe1)
                {

                }
                else
                {

                }
                break;
            case -2:

                break;
            case -3:

                break;
            case -4:

                break;
            default:

                break;
        }*/

        

        
        





    }
    
}
