using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class testPlayerScript : MonoBehaviour
{
    
    private Rigidbody2D rigidBody;
    private SpriteRenderer sprite;
    
    //[SerializeField]

    private float moveConst = 200;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = Vector3.zero;
        if (Input.GetKey("w"))
        {
            rigidBody.AddForce(transform.up* moveConst * Time.deltaTime);
            //Debug.Log("w");
            sprite.flipY = false;

            if (Input.GetKey("a"))
            {
                rigidBody.AddForce(transform.right* -moveConst * Time.deltaTime);
                //Debug.Log("a");
                sprite.flipX = true;
            }
            else if (Input.GetKey("d"))
            {
                rigidBody.AddForce(transform.right* moveConst * Time.deltaTime);
                //Debug.Log("d");
                sprite.flipX = false;
            }
            else if (Input.GetKey("s"))
            {
                rigidBody.AddForce(transform.up* -moveConst * Time.deltaTime);
                //Debug.Log("s");
                sprite.flipY = true;
            }

        }

        else if (Input.GetKey("a"))
        {
            rigidBody.AddForce(transform.right* -moveConst * Time.deltaTime);
            //Debug.Log("a");
            sprite.flipX = true;

            if (Input.GetKey("w"))
            {
                rigidBody.AddForce(transform.up* moveConst * Time.deltaTime);
                //Debug.Log("w");
                sprite.flipY = false;
            }
            else if (Input.GetKey("d"))
            {
                rigidBody.AddForce(transform.right* moveConst * Time.deltaTime);
                //Debug.Log("d");
                sprite.flipX = false;
            }
            else if (Input.GetKey("s"))
            {
                rigidBody.AddForce(transform.up* -moveConst * Time.deltaTime);
                //Debug.Log("s");
                sprite.flipY = true;
            }

        }
        else if (Input.GetKey("d"))
        {
            rigidBody.AddForce(transform.right* moveConst * Time.deltaTime);
            //Debug.Log("d");
            sprite.flipX = false;

            if (Input.GetKey("w"))
            {
                rigidBody.AddForce(transform.up* moveConst * Time.deltaTime);
                //Debug.Log("w");
                sprite.flipY = false;
            }
            else if (Input.GetKey("d"))
            {
                rigidBody.AddForce(transform.right* moveConst * Time.deltaTime);
                //Debug.Log("d");
                sprite.flipX = false;
            }
            else if (Input.GetKey("s"))
            {
                rigidBody.AddForce(transform.up* -moveConst * Time.deltaTime);
                //Debug.Log("s");
                sprite.flipY = true;
            }
        }
        else if (Input.GetKey("s"))
        {
            rigidBody.AddForce(transform.up* -moveConst * Time.deltaTime);
            //Debug.Log("s");
            sprite.flipY = true;

            if (Input.GetKey("w"))
            {
                rigidBody.AddForce(transform.up* moveConst * Time.deltaTime);
                //Debug.Log("w");
                sprite.flipY = false;
            }
            else if (Input.GetKey("d"))
            {
                rigidBody.AddForce(transform.right* moveConst * Time.deltaTime);
                //Debug.Log("d");
                sprite.flipX = false;
            }
            else if (Input.GetKey("s"))
            {
                rigidBody.AddForce(transform.up* -moveConst * Time.deltaTime);
                //Debug.Log("s");
                sprite.flipY = true;
            }
        }
    }
}
