
using UnityEngine;
using System;
using TMPro;

public class GameTimerAndInfo : MonoBehaviour
{
// 
    public float Seconds;
    public int Minutes;
    public TMP_Text Timertext;
    //public string CurrentTimer;
    
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
        //CurrentTimer = Minutes.ToString() + ":" + Math.Floor(Seconds).ToString();
        Timertext.text =  Minutes.ToString() + ":" + Math.Floor(Seconds).ToString(); 
        
    }
}
