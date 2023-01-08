using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class ClockTimer : Singleton<ClockTimer>
{
    public float timer = 480;
    [SerializeField]
    protected float timeMultiplier = 2;
    protected TextMeshProUGUI text; 


    public float TimeMultiplier => timeMultiplier;
    void Start()
    {
        text = this.GetComponentInChildren<TextMeshProUGUI>(); 
    }

    

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime * timeMultiplier;

            int minutes = (int)timer / 60;
            int seconds = (int)timer % 60;
            text.text = minutes + ":" + seconds.ToString("00");
            if (timer <= 0)
            {
                GameController.Instance.ActivateNextDayButtonOnTimer(); 
            }
        }

    }
}
