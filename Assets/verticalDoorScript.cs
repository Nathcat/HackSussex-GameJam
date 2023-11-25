using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class verticalDoorScript : MonoBehaviour
{
    
    private byte id;
    private Rigidbody2D rigidBody;
    private BoxCollider2D collider;
    
    
    private double closedPos;
    
    private double openPos;
    
    // Start is called before the first frame update
    void Start()
    {
        
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        closedPos = gameObject.transform.position.y;
        openPos = gameObject.transform.position.y + 3;
    }

    // Update is called once per frame
    void Update()
    {
        if((id & 0x80) == 0x80)
        {
            
            if(gameObject.transform.position.y >= openPos)
            {
                rigidBody.velocity = Vector3.zero;
                
                //Debug.Log("a");
            }
            else
            {
                rigidBody.AddForce(transform.up * 100 * Time.deltaTime);
                collider.enabled = false;

            }

        }
        else
        {
            

            if(gameObject.transform.position.y <= closedPos)
            {
                rigidBody.velocity = Vector3.zero;
                
                //Debug.Log("a");
            }
            else
            {
                rigidBody.AddForce(transform.up* -100 * Time.deltaTime);
                collider.enabled = true;

            }
        }
    }

    public bool checkId(byte check)
    {
        return (id & 0x7F) == (check & 0x7F);
    }
}
