using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions; 

public class Tweet : MonoBehaviour
{
    [SerializeField]
    protected Image avatarImage;
    [SerializeField]
    protected TextMeshProUGUI userNameText, handleText, tweetText;

    [HideInInspector]
    public User user;

    [HideInInspector]
    public string searchableText = "";

    private Button button; 

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(OnTweetClick); 
    }

    public void OnTweetClick()
    {
        PopupPanel.Instance.Show("Do you realy want to flag this post for review? \n This decision is permanent and can't be reversed later", () =>
        {
            GameController.Instance.FlagTweet(this); 
        }); 
    }


    public void Flag()
    {
        button.interactable = false; 
    }


    public bool CorrectlyReported(List<string> generalWordsBanned)
    {
        if (user.isCeo || !user.isCoworker)
        {
            return false; 
        }
        for (int i = 0; i<generalWordsBanned.Count; i++)
        {
            Regex regex = new Regex(generalWordsBanned[i], RegexOptions.IgnoreCase); 
            if (regex.IsMatch(searchableText))
            {
                return true; 
            }
        }
        for (int i = 0; i< user.concreteFlaggedWords.Count; i++)
        {
            Regex regex = new Regex(user.concreteFlaggedWords[i], RegexOptions.IgnoreCase);
            if (regex.IsMatch(searchableText))
            {
                return true;
            }
        }

        return false; 
    }
    public void InitializeTweetWithUser(User user, int tweetIndex)
    {
        button.interactable = true; 
        avatarImage.sprite = user.avatar;
        this.user = user;
        userNameText.text = user.username;
        handleText.text = user.handle;
        tweetText.text = user.possibleRandomTweets[tweetIndex]; 
        searchableText = user.username + "@" + user.handle + user.possibleRandomTweets[tweetIndex]; 
    }

    public void InitializeRandomTweet(User user, string randomTweet)
    {
        button.interactable = true; 
        avatarImage.sprite = user.avatar;
        this.user = user;
        userNameText.text = user.username;
        handleText.text = "@" + user.handle;
        tweetText.text = randomTweet;
        searchableText = user.username + "@" + user.handle + randomTweet;
    }
   
    public void FilterByText(Regex filter)
    {
        gameObject.SetActive(filter.IsMatch(searchableText)); 
    }
}
