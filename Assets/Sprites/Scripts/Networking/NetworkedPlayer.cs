using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedPlayer : MonoBehaviour
{
    public byte clientID;
    public Vector3 position = Vector3.zero;
    public bool allowSet = true;

    public void Update() {
        if (allowSet) {
            transform.position = position;
        }
    }
}
