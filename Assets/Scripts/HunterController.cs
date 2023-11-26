using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterController : MonoBehaviour
{
    public float killRadius = 5f;

    public void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, killRadius);

            foreach (Collider2D collider in colliders) {
                NetworkedPlayer networkController;
                if (collider == gameObject.GetComponent<Collider2D>()) continue;
                if ((networkController = collider.gameObject.GetComponent<NetworkedPlayer>()) != null) {
                    networkController.Kill();
                    break;
                }
            }
        }
    }
}
