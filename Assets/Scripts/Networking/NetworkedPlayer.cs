using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedPlayer : MonoBehaviour
{
    public byte clientID;
    public Vector3 position = Vector3.zero;
    public Vector3 prevFrame = Vector3.zero;
    public bool allowSet = true;
    public NetworkClient client;
    public SpriteRenderer spriteR;
    public Sprite[] spriteArray; 

    public static Vector3 copyVector(Vector3 a)
    {
        return new Vector3(
            a.x,
            a.y,
            a.z
        );
    }


    void Start()
    {
        
        spriteR = GetComponent<SpriteRenderer>();
        
    }

    public void Update() {
        if (allowSet) {
            transform.position = position;

            if(transform.position.x - prevFrame.x != 0)
            {
                spriteR.sprite = spriteArray[0];
                if(transform.position.x - prevFrame.x > 0)
                {
                    spriteR.flipX = true;
                }
                else
                {
                    spriteR.flipX = true;
                }
            }
            else if(transform.position.y - prevFrame.y != 0)
            {
                if(transform.position.y - prevFrame.y > 0)
                {
                    spriteR.sprite = spriteArray[2];
                }
                else
                {
                    spriteR.sprite = spriteArray[1];
                }
            }

            if (client.hunterID == clientID) {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<ParticleSystem>().Play();
            }
            else {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.GetComponent<ParticleSystem>().Stop();
            }

            prevFrame = copyVector(position);
        }
    }

    public void Kill() {
        Debug.Log("Survivor: " + gameObject.name + " killed!");
        client.SendKillPacket(this);
        Destroy(gameObject);
    }
}
