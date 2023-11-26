using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public byte id;
    public bool Complete = false;

    public bool complete {
        get { return Complete; }
        set {
            Complete = value;
            GameObject.FindGameObjectsWithTag("netclient")[0].GetComponent<NetworkClient>().SendTaskCompletePacket(id);
        }
    }

    public virtual void load();
}
