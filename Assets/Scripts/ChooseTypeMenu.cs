using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseTypeMenu : MonoBehaviour
{
    public void ControllerButtonClicked() {
        SceneManager.LoadScene("Controller");
    }

    public void SurvivorButtonClicked() {
        SceneManager.LoadScene("Survivor");
    }
}
