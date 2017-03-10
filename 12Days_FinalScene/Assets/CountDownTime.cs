using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTime : MonoBehaviour
{
    public static int countdownTime = 10;

    // Use this for initialization
    private void Start()
    {
        InvokeRepeating("CountDown", 0, 1);
        Invoke("Quit", 10f);
    }

    private void CountDown()
    {
        countdownTime--;
    }

    private void Quit()
    {
        Application.Quit();
    }
}