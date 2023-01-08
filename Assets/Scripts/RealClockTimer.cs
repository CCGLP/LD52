using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class RealClockTimer : Singleton<RealClockTimer>
{
    public float timer = 480;

    protected TextMeshProUGUI text;

    void Start()
    {
        text = this.GetComponentInChildren<TextMeshProUGUI>();
    }



    void Update()
    {
        if (timer < 1440 && GameController.Instance.CanUseTimer)
        {
            timer += Time.deltaTime * ClockTimer.Instance.TimeMultiplier;

            int minutes = (int)timer / 60;
            int seconds = (int)timer % 60;
            text.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            if (timer >= 1440)
            {
                timer = 1440;
                PopupPanel.Instance.ShowSpecialEndOfDay(); 
            }
        }

    }
}
