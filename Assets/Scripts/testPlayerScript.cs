using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class testPlayerScript : MonoBehaviour
{
    
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteR;
    public Sprite[] spriteArray;
    private int order = 0;


    private float moveConst = 200;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = Vector3.zero;
        if (Input.GetKey("w"))
        {
            rigidBody.AddForce(transform.up* moveConst * Time.deltaTime);
            //Debug.Log("w");
            spriteR.sprite = spriteArray[2];
            spriteR.sortingOrder = order--; 

            if (Input.GetKey("a"))
            {
                rigidBody.AddForce(transform.right* -moveConst * Time.deltaTime);
                //Debug.Log("a");
                spriteR.sprite = spriteArray[0];
                spriteR.flipX = false;
            }
            else if (Input.GetKey("d"))
            {
                rigidBody.AddForce(transform.right* moveConst * Time.deltaTime);
                //Debug.Log("d");
                spriteR.sprite = spriteArray[0];
                spriteR.flipX = true;
            }
            else if (Input.GetKey("s"))
            {
                rigidBody.AddForce(transform.up* -moveConst * Time.deltaTime);
                //Debug.Log("s");
                spriteR.sprite = spriteArray[1];
                //sprite.flipY = true;
                spriteR.sortingOrder = order++; 
            }

        }

        else if (Input.GetKey("a"))
        {
            rigidBody.AddForce(transform.right* -moveConst * Time.deltaTime);
            //Debug.Log("a");
            spriteR.sprite = spriteArray[0];
            spriteR.flipX = false;

            if (Input.GetKey("w"))
            {
                rigidBody.AddForce(transform.up* moveConst * Time.deltaTime);
                //Debug.Log("w");
                spriteR.sprite = spriteArray[2];
                spriteR.sortingOrder = order--; 
            }
            else if (Input.GetKey("d"))
            {
                rigidBody.AddForce(transform.right* moveConst * Time.deltaTime);
                //Debug.Log("d");
                spriteR.sprite = spriteArray[0];
                spriteR.flipX = true;
            }
            else if (Input.GetKey("s"))
            {
                rigidBody.AddForce(transform.up* -moveConst * Time.deltaTime);
                //Debug.Log("s");
                spriteR.sprite = spriteArray[1];
                spriteR.sortingOrder = order++;
            }

        }
        else if (Input.GetKey("d"))
        {
            rigidBody.AddForce(transform.right* moveConst * Time.deltaTime);
            //Debug.Log("d");
            spriteR.sprite = spriteArray[0];
            spriteR.flipX = true;

            if (Input.GetKey("w"))
            {
                rigidBody.AddForce(transform.up* moveConst * Time.deltaTime);
                //Debug.Log("w");
                spriteR.sprite = spriteArray[2];
                spriteR.sortingOrder = order--; 
            }
            else if (Input.GetKey("a"))
            {
                rigidBody.AddForce(transform.right* -moveConst * Time.deltaTime);
                //Debug.Log("d");
                spriteR.sprite = spriteArray[0];
                spriteR.flipX = false;
            }
            else if (Input.GetKey("s"))
            {
                rigidBody.AddForce(transform.up* -moveConst * Time.deltaTime);
                //Debug.Log("s");
                spriteR.sprite = spriteArray[1];
                spriteR.sortingOrder = order++; 
            }
        }
        else if (Input.GetKey("s"))
        {
            rigidBody.AddForce(transform.up* -moveConst * Time.deltaTime);
            //Debug.Log("s");
            spriteR.sprite = spriteArray[1];
            spriteR.sortingOrder = order++;

            if (Input.GetKey("w"))
            {
                rigidBody.AddForce(transform.up* moveConst * Time.deltaTime);
                //Debug.Log("w");
                spriteR.sprite = spriteArray[2];
                spriteR.sortingOrder = order--; 
            }
            else if (Input.GetKey("d"))
            {
                rigidBody.AddForce(transform.right* moveConst * Time.deltaTime);
                //Debug.Log("d");
                spriteR.sprite = spriteArray[0];
                spriteR.flipX = true;
            }
            else if (Input.GetKey("a"))
            {
                rigidBody.AddForce(transform.right* -moveConst * Time.deltaTime);
                //Debug.Log("a");
                spriteR.sprite = spriteArray[0];
                spriteR.flipX = false;
            }
        }
    }
}
