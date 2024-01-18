using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameTimer : MonoBehaviour
{
// 
    private float Seconds;
    private int Minutes;
    public GameObject Timer;
    public string CurrentTimer;
    
    void Start()
    {
        Seconds = 0;
        Minutes = 0;
    }

    
    void Update()
    {
        Seconds += Time.deltaTime;
        if(Seconds >= 60) {
        Minutes +=1;
        Seconds = 0;
        }
        CurrentTimer = Minutes.ToString() + ":" + Math.Floor(Seconds).ToString();
        Timer.GetComponent<TextMeshProUGUI>().text = "Timer : " + Minutes.ToString() + ":" + Math.Floor(Seconds).ToString(); 
        
    }
}
