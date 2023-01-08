using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening; 

public class PopupPanel : Singleton<PopupPanel>
{
    [SerializeField]
    protected TextMeshProUGUI infoText;
    [SerializeField]
    protected Button yesButton, noButton;
    [SerializeField]
    private float fadeAnimationTime = 0.2f; 
    protected CanvasGroup cg;


    protected Action okAction, noAction; 
    void Start()
    {
        cg = this.GetComponent<CanvasGroup>();
        yesButton.onClick.AddListener(OnYesButton);
        noButton.onClick.AddListener(OnNoButton); 
        okAction = () =>
        {
            noButton.gameObject.SetActive(true);
            yesButton.GetComponentInChildren<TextMeshProUGUI>().text = "YES";
        };
    }
    public void ShowSpecialEndOfDay()
    {
        yesButton.GetComponentInChildren<TextMeshProUGUI>().text = "OK";
        noButton.gameObject.SetActive(false);

        Show("It's midnight. The janitor of the building sends you to sleep for today.", () =>
        {
            noButton.gameObject.SetActive(true);
            yesButton.GetComponentInChildren<TextMeshProUGUI>().text = "YES";
            GameController.Instance.GoToInterlude(); 
        });
       
    }
    public void OnYesButton() 
    {
        Hide();
        okAction?.Invoke(); 
    }

    public void OnNoButton()
    {
        Hide();
        noAction?.Invoke(); 
    }

    public void Show(string text, Action okAction = null, Action noAction = null)
    {
        infoText.text = text;
        this.okAction = okAction;
        this.noAction = noAction; 
        cg.interactable = true;
        cg.blocksRaycasts = true;
        cg.DOFade(1, fadeAnimationTime);
    }

    public void Hide()
    {
        cg.interactable = false;
        cg.blocksRaycasts = false;
        cg.DOFade(0, fadeAnimationTime);
    }
}
