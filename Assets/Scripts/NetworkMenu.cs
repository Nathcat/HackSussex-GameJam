using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NetworkMenu : MonoBehaviour
{
    public NetworkServer server;
    public TMP_Text[] playerConnectedTexts = new TMP_Text[6];
    public GameObject controllerMap;

    public void Start() {
        controllerMap.SetActive(false);
    }

    public void Update() {
        for (int i = 0; i < 6; i++) {
            if (server.clientIDs[i] != 0) {
                playerConnectedTexts[i].text = "Player " + i + " - Connected";
            }
            else {
                playerConnectedTexts[i].text = "Player " + i + " - Not connected";
            }
        }
    }

    public void StartGameButtonClicked() {
        server.networkState = NetworkServer.STATE_PLAYING;
        controllerMap.SetActive(true);
        gameObject.SetActive(false);
    }
}
