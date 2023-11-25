using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class task_manager : MonoBehaviour
{
    // Start is called before the first frame update
    public int tasks;
    void Start()
    {
        tasks = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (tasks == 10) {
            SceneManager.LoadScene("Win");
        }
    }
}
