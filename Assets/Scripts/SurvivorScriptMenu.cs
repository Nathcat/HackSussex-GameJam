using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SurvivorScriptMenu : MonoBehaviour
{
    public TMP_InputField ipAddress;
    public TMP_Text buttonText;
    public NetworkClient client;

    public void ConnectButtonClicked() {
        client.serverHostname = ipAddress.text;
        client.StartClient();

        buttonText.text = "Connecting...";
        buttonText.transform.parent.gameObject.GetComponent<Button>().enabled = false;

        StartCoroutine(WaitTillConnected());
    }

    public IEnumerator WaitTillConnected() {
        while (!client.connected) {
            yield return new WaitForSeconds(0.01f);
        }

        gameObject.SetActive(false);
    }
}
