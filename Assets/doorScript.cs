using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    
    private int id;
    private Rigidbody2D rigidBody;
    private BoxCollider2D collider;
    
    
    
    [SerializeField]
    private bool open;
    //[SerializeField]
    private double closedPos;
    //[SerializeField]
    private double openPos;


    // Start is called before the first frame update
    void Start()
    {
        open = true;
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        closedPos = gameObject.transform.position.x;
        openPos = gameObject.transform.position.x + 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(open == true)
        {
            
            if(gameObject.transform.position.x >= openPos)
            {
                rigidBody.velocity = Vector3.zero;
                
                //Debug.Log("a");
            }
            else
            {
                rigidBody.AddForce(transform.right * 100 * Time.deltaTime);
                collider.enabled = false;

            }

        }
        else
        {
            

            if(gameObject.transform.position.x <= closedPos)
            {
                rigidBody.velocity = Vector3.zero;
                
                //Debug.Log("a");
            }
            else
            {
                rigidBody.AddForce(transform.right* -100 * Time.deltaTime);
                collider.enabled = true;

            }
        }
    }





    public void Open()
    {
        open = true;
    }

    public void Close()
    {
        open = false;
    }
}
