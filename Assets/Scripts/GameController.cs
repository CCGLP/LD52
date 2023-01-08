using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Text.RegularExpressions;


public class GameController : Singleton<GameController>
{
    [SerializeField]
    private Button nextDayButtonGame,nextDayButtonInterlude, searchButton;

    [SerializeField]
    private TMP_InputField searchInputField; 

    [SerializeField]
    private CanvasGroup gameCg, interludeCg;

    [SerializeField]
    private GameObject tweetPrefab;

    [SerializeField]
    private RectTransform tweetContainer;

    [SerializeField]
    private RandomTweetsData randomTweetsData;

    [SerializeField]
    private List<User> users;

    protected List<User> usableUsers; 

    [SerializeField]
    private float fadeAnimationTime = 0.2f;

    [SerializeField]
    private List<string> generalBannedWords; 

    protected PoolManager<Tweet> tweetPool;

    protected List<Tweet> activeTweets;
    protected List<Tweet> reportedTweets; 

    protected int dayIndex = -1; 

    void Start()
    {
        activeTweets = new List<Tweet>();
        reportedTweets = new List<Tweet>(); 
        tweetPool = new PoolManager<Tweet>();
        tweetPool.PoolQuantity = 100;
        tweetPool.InitializePool(tweetPrefab);
        usableUsers = new List<User>(); 
        for (int i = 0; i< users.Count; i++)
        {
            usableUsers.Add(Instantiate(users[i])); 
        }
        nextDayButtonGame.onClick.AddListener(GoToInterlude);
        nextDayButtonInterlude.onClick.AddListener(GameStartRoutine);
        searchButton.onClick.AddListener(FilterTweetsByInputString); 
        GameStartRoutine();     
    }


    public void GoToInterlude()
    {
        ReviewProcess(); 
        ShowCanvasGroup(interludeCg);
        HideCanvasGroup(gameCg);
    }

    private void ReviewProcess()
    {
        int numberOfReports = reportedTweets.Count;
        int extraHoursBonus = ((int)(RealClockTimer.Instance.timer - 480 - 480)) / 100;

        int goodReports = 0;
        int badReports = 0; 

        bool isGameEnd = false;

        for (int i = 0; i< reportedTweets.Count; i++)
        {
            bool correct = reportedTweets[i].CorrectlyReported(generalBannedWords);
            if (correct) goodReports++;
            else badReports++; 
        }

        if (goodReports < badReports)
        {
            isGameEnd = true; 
        }

        ReviewPanel.Instance.SetInformation(numberOfReports, goodReports, badReports, extraHoursBonus, !isGameEnd); 

    }
    private void GameStartRoutine()
    {
        dayIndex++;
        reportedTweets.Clear(); 
        CleanPreviousDaysTweets();
        InstantiateNewDayTweets();
        ShuffleTweetsOrder(); 
        ClockTimer.Instance.timer = 480;
        RealClockTimer.Instance.timer = 480; 
        nextDayButtonGame.interactable = false;
        ShowCanvasGroup(gameCg);
        HideCanvasGroup(interludeCg);
    }

    private void ShuffleTweetsOrder()
    {
        for (int i = 0; i< activeTweets.Count; i++)
        {
            activeTweets[i].transform.SetSiblingIndex(Random.Range(0, activeTweets.Count - 1));
        }
    }

    private void InstantiateNewDayTweets()
    {

        List<string> randomTweetsToday = new List<string>(randomTweetsData.tweets); 
        for (int i = 0; i < usableUsers.Count; i++)
        {
            for (int j = 0; j < usableUsers[i].tweetsPerDay; j++)
            {
               
                if (usableUsers[i].possibleRandomTweets.Count > 0)
                {
                    var tweet = tweetPool.InstantiateObject();
                    tweet.transform.SetParent(tweetContainer);
                    tweet.gameObject.SetActive(true);
                    tweet.transform.localScale = Vector3.one;
                    int randomTweet = Random.Range(0, usableUsers[i].possibleRandomTweets.Count);
                    tweet.InitializeTweetWithUser(usableUsers[i], randomTweet);

                    Debug.Log("previous usable tweets from user: " + usableUsers[i].username + " : " + usableUsers[i].possibleRandomTweets.Count); 
                    usableUsers[i].possibleRandomTweets.RemoveAt(randomTweet);
                    Debug.Log("post usable tweets from user: " + usableUsers[i].username + " : " + usableUsers[i].possibleRandomTweets.Count);
                    activeTweets.Add(tweet);


                }
                else if (randomTweetsToday.Count > 0 && usableUsers[i].canUseRandomGeneralTweets)
                {
                    var tweet = tweetPool.InstantiateObject();
                    tweet.transform.SetParent(tweetContainer);
                    tweet.gameObject.SetActive(true);
                    tweet.transform.localScale = Vector3.one;
                    int randomIndex = Random.Range(0, randomTweetsToday.Count); 
                    tweet.InitializeRandomTweet(usableUsers[i], randomTweetsToday[randomIndex]);
                    randomTweetsToday.RemoveAt(randomIndex);
                    activeTweets.Add(tweet);


                }

               

            }

            if (usableUsers[i].isCoworker && usableUsers[i].goodStoryLineTweets.Count > 0)
            {
                List<string> dayTweets = usableUsers[i].goodStoryLineTweets[dayIndex].list;
                for (int j = 0; j < dayTweets.Count; j++)
                {
                    var tweet = tweetPool.InstantiateObject();
                    tweet.transform.SetParent(tweetContainer);
                    tweet.gameObject.SetActive(true);
                    tweet.transform.localScale = Vector3.one;
                    tweet.InitializeRandomTweet(usableUsers[i], dayTweets[j]);
                    activeTweets.Add(tweet); 
                }
            }
            else if (!usableUsers[i].isCoworker && usableUsers[i].badStoryLineTweets.Count > 0)
            {
                List<string> dayTweets = usableUsers[i].badStoryLineTweets[dayIndex].list;
                for (int j = 0; j < dayTweets.Count; j++)
                {
                    var tweet = tweetPool.InstantiateObject();
                    tweet.transform.SetParent(tweetContainer);
                    tweet.gameObject.SetActive(true);
                    tweet.transform.localScale = Vector3.one;
                    tweet.InitializeRandomTweet(usableUsers[i], dayTweets[j]);
                    activeTweets.Add(tweet);
                }
            }


        }
    }

    private void CleanPreviousDaysTweets()
    {
        for (int i = 0; i< activeTweets.Count; i++)
        {
            tweetPool.ReturnToPool(activeTweets[i]);
        }
        activeTweets.Clear(); 
    }

    public void FilterTweetsByInputString()
    {
        if (searchInputField.text.Length > 0) {
            Regex regex = new Regex(searchInputField.text); 
            for (int i = 0; i < activeTweets.Count; i++)
            {
                activeTweets[i].FilterByText(regex); 
            }
        }
        else
        {
            for (int i = 0; i< activeTweets.Count; i++)
            {
                activeTweets[i].gameObject.SetActive(true); 
            }
        }
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

    public void FlagTweet(Tweet tweet)
    {
        tweet.Flag();
        reportedTweets.Add(tweet);
    }

    public void ActivateNextDayButtonOnTimer()
    {
        nextDayButtonGame.interactable = true; 

    }
}
