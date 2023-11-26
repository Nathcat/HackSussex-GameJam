using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationTester : MonoBehaviour
{
    void Start()
    {
        GetComponent<WorldGenerator>().Generate(Random.seed);
    }
}
