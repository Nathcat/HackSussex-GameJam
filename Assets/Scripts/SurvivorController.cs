using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorController : MonoBehaviour
{
    public void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Collider2D[] colliders = Physics2D.OverlapCircle(transform.position, 2.5f);
            
            foreach (Collider2D collider in colliders) {
                Task task;
                if ((task = collider.gameObject.GetComponent<task>()) != null) {
                    task.load();
                    break;
                }
            }
        }
    }
}
