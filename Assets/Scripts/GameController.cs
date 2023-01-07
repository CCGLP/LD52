using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; 

public class GameController : Singleton<GameController>
{
    [SerializeField]
    private Button nextDayButtonGame,nextDayButtonInterlude;

    [SerializeField]
    private CanvasGroup gameCg, interludeCg;

    [SerializeField]
    private float fadeAnimationTime = 0.2f; 

    void Start()
    {
        GameStartRoutine();
        nextDayButtonGame.onClick.AddListener(GoToInterlude);
        nextDayButtonInterlude.onClick.AddListener(GameStartRoutine);
    }


    public void GoToInterlude()
    {
        ShowCanvasGroup(interludeCg);
        HideCanvasGroup(gameCg);
    }
    private void GameStartRoutine()
    {
        ClockTimer.Instance.timer = 480; 
        nextDayButtonGame.interactable = false;
        ShowCanvasGroup(gameCg);
        HideCanvasGroup(interludeCg);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShowCanvasGroup(CanvasGroup cg)
    {
        cg.interactable = true;
        cg.blocksRaycasts = true;
        cg.DOFade(1, fadeAnimationTime);
    }

    public void HideCanvasGroup(CanvasGroup cg)
    {
        cg.interactable = false;
        cg.blocksRaycasts = false;
        cg.DOFade(0, fadeAnimationTime);
    }

    public void ActivateNextDayButtonOnTimer()
    {
        nextDayButtonGame.interactable = true; 

    }
}
