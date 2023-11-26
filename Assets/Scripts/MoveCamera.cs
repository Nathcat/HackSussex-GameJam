using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float moveSpeed = 1f;

    public void Update() {
        transform.Translate(Vector3.up * Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed);
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed);
    }
}
