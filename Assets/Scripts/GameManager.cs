using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public NetworkServer networkServer;
    public WorldGenerator worldGenerator;


    public void Start() {
        int seed = Random.Range((int) 0, (int) 0x7FFFFFFF);

        if (worldGenerator != null) worldGenerator.Generate(seed);
        if (networkServer != null) networkServer.StartServer(seed);
    }
}
