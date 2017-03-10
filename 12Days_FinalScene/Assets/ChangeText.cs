using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeText : MonoBehaviour
{
    public TextMesh textCountDown;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(CountDownTime.countdownTime.ToString());
        textCountDown.text = CountDownTime.countdownTime.ToString() + "s !";
    }
}