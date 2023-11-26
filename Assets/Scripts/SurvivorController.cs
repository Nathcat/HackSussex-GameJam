using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                        task.gameObject.SetActive(true);
                        task.load();
                        break;
                    }
                }
            }

            GameObject[] tasklocs = GameObject.FindGameObjectsWithTag("task");
            bool won = true;
            foreach (GameObject task in tasklocs) {
                if (!task.GetComponent<TaskLocation>().GetTask().complete) {
                    won = false;
                }
            }

            if (won) {
                SceneManager.LoadScene("Win");
            }
        }
    }
}
