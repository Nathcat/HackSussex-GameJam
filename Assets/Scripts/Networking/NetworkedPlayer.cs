using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedPlayer : MonoBehaviour
{
    public byte clientID;
    public Vector3 position = Vector3.zero;
    public bool allowSet = true;
    public NetworkClient client;

    public void Update() {
        if (allowSet) {
            transform.position = position;

            if (client.hunterID == clientID) {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.GetComponent<ParticleSystem>().Play();
            }
            else {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.GetComponent<ParticleSystem>().Stop();
            }
        }
    }

    public void Kill() {
        Debug.Log("Survivor: " + gameObject.name + " killed!");
        client.SendKillPacket(this);
        Destroy(gameObject);
    }
}
