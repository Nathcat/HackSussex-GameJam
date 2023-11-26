using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class fishing : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text text;
    public bool hook = false;
    public bool pulling = false;
    public bool fishes = false;
    public Slider slide;
    public GameObject eventsystem;
    public GameObject host;
    public GameObject fishy;

    public void load()
    {
        fishy.gameObject.SetActive(false);
        StartCoroutine(fish());
    }

    // Update is called once per frame
    private void Update()
    {
        if (pulling)
        {
            slide.value -= 0.1f * Time.deltaTime;
            if (slide.value <= 0) {
                text.text = "lost";
                pulling = false;
                hook = false;
                fishy.gameObject.SetActive(false);
                slide.value = 0.25f;
                StartCoroutine(fish());
            }
        }
    }
    public IEnumerator fish() {
        yield return new WaitForSeconds(3);
        fishy.gameObject.SetActive(true);
        text.text = "hook the fish";
        fishes = true;
        yield return new WaitForSeconds(3);
        if (!hook) {
            text.text = "missed";
            fishy.gameObject.SetActive(false);
            StartCoroutine(fish());
        }
    }
    public void reel() {
        if (fishes == true)
        {
            hook = true;
            pulling = true;
            slide.value += 0.1f;
            if (slide.value >= 1)
            {
                eventsystem.GetComponent<task_manager>().tasks += 1;
                host.gameObject.SetActive(false);
            }
        }
    }
}
