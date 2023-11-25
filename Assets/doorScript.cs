using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    public bool vertical;
    public byte id;
    //public bool open;
    private Rigidbody2D rigidBody;
    private BoxCollider2D collider;
    private float closedPos;
    private float openPos;


    


    // Start is called before the first frame update
    void Start()
    {
        
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();

        if(vertical)
        {
            closedPos = gameObject.transform.position.y;
            openPos = gameObject.transform.position.y + 3;
        }
        else
        {
            closedPos = gameObject.transform.position.x;
            openPos = gameObject.transform.position.x + 3;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(vertical)
        {
           
           if((id & 0x80) == 0x80)
            {
                collider.enabled = false;   
                if(gameObject.transform.position.y >= openPos)
                {
                    //rigidBody.velocity = Vector3.zero;
                    float deltaY = openPos - transform.position.y;
                    deltaY *= Time.deltaTime;
                    transform.Translate(new Vector3(0, deltaY, 0));
                    
                    //Debug.Log("a");
                }
                else
                {
                    //rigidBody.AddForce(transform.up * 100 * Time.deltaTime);]
                    float deltaY = openPos - transform.position.y;
                    deltaY *= Time.deltaTime;
                    transform.Translate(new Vector3(0, deltaY, 0));

                }

            }
            else
            {
                
                collider.enabled = true;
                if(gameObject.transform.position.y <= closedPos)
                {
                    float deltaY = closedPos - transform.position.y;
                    deltaY *= Time.deltaTime;
                    transform.Translate(new Vector3(0, deltaY, 0));
                    
                    //Debug.Log("a");
                }
                else
                {
                    float deltaY = closedPos - transform.position.y;
                    deltaY *= Time.deltaTime;
                    transform.Translate(new Vector3(0, deltaY, 0));

                }
            } 
        }
        else
        {
            if((id & 0x80) == 0x80)
            {
                collider.enabled = false;   
                if(gameObject.transform.position.x >= openPos)
                {
                    //rigidBody.velocity = Vector3.zero;
                    float deltaX = openPos - transform.position.x;
                    deltaX *= Time.deltaTime;
                    transform.Translate(new Vector3(deltaX, 0, 0));
                    
                    //Debug.Log("a");
                }
                else
                {
                    //rigidBody.AddForce(transform.up * 100 * Time.deltaTime);]
                    float deltaX = openPos - transform.position.x;
                    deltaX *= Time.deltaTime;
                    transform.Translate(new Vector3(deltaX, 0, 0));

                }

            }
            else
            {
                
                collider.enabled = true;
                if(gameObject.transform.position.x <= closedPos)
                {
                    float deltaX = closedPos - transform.position.x;
                    deltaX *= Time.deltaTime;
                    transform.Translate(new Vector3(deltaX, 0, 0));
                    
                    //Debug.Log("a");
                }
                else
                {
                    float deltaX = closedPos - transform.position.x;
                    deltaX *= Time.deltaTime;
                    transform.Translate(new Vector3(deltaX, 0, 0));

                }
            } 
        }
        
    }

    public bool checkId(byte check)
    {
        return (id & 0x7F) == (check & 0x7F);
    }



    
}
