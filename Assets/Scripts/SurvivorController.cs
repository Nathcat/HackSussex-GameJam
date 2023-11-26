using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorController : MonoBehaviour
{
    public void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2.5f);
            
            foreach (Collider2D collider in colliders) {
                TaskLocation taskloc;
                if ((taskloc = collider.gameObject.GetComponent<TaskLocation>()) != null) {
                    Task task = taskloc.GetTask();
                    if (!task.complete) {
                        task.load();
                        break;
                    }
                }
            }
        }
    }
}
