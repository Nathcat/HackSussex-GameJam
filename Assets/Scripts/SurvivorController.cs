using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorController : MonoBehaviour
{
    public void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2.5f);
            
            foreach (Collider2D collider in colliders) {
                Debug.Log(collider.gameObject);
                TaskLocation taskloc = collider.gameObject.GetComponent<TaskLocation>();
                if (taskloc != null) {
                    Debug.Log("Got task location");
                    Task task = taskloc.GetTask();
                    if (!task.complete) {
                        Debug.Log("load task");
                        task.load();
                        break;
                    }
                }
            }
        }
    }
}
