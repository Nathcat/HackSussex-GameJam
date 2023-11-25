using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedPlayer : MonoBehaviour
{
    public byte clientID;
    public Vector3 position = Vector3.zero;

    public void Update() {
        transform.position = position;
    }
}
