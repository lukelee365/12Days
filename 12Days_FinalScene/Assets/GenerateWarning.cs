using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWarning : MonoBehaviour
{
    public GameObject warning;
    public float interval;
    public float randomInterval;
    public Vector2 randomValue;
    private GameObject previous;

    // Use this for initialization
    private void Start()
    {
        previous = warning;
        InvokeRepeating("InitWarning", interval, interval);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void InitWarning()
    {
        if (warning != null)
            previous = Instantiate(warning, warning.transform.position + Vector3.up * (int)Random.Range(randomValue.x, randomValue.y) + Vector3.right * (int)Random.Range(randomValue.x, randomValue.y) + Vector3.forward * (int)Random.Range(randomValue.x, randomValue.y), warning.transform.rotation) as GameObject;
    }
}