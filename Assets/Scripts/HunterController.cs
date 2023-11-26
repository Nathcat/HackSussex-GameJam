using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterController : MonoBehaviour
{
    public float killRadius = 5f;
    public bool allowKill = true;

    public void Update() {
        if (Input.GetMouseButtonDown(0) && allowKill) {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, killRadius);

            foreach (Collider2D collider in colliders) {
                NetworkedPlayer networkController;
                if (collider == gameObject.GetComponent<Collider2D>()) continue;
                if ((networkController = collider.gameObject.GetComponent<NetworkedPlayer>()) != null) {
                    networkController.Kill();
                    StartCoroutine(KillCooldown());

                    GameObject[] hunterSpawns = GameObject.FindObjectsWithTag("hunterspawn");
                    GameObject spawn = hunterSpawns[Random.Range(0, hunterSpawns.Length)];
                    transform.position = spawn.transform.position;
                    break;
                }
            }
        }
    }

    private IEnumerator KillCooldown() {
        allowKill = false;
        yield return new WaitForSeconds(15f);
        allowKill = true;
    }
}
