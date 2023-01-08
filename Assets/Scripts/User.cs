using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "user", menuName ="LD52/User", order = 1)]
public class User : ScriptableObject
{
    [SerializeField]
    public bool isCoworker = false;
    [SerializeField]
    public Sprite avatar;
    [SerializeField]
    public string username;
    [SerializeField]
    public string handle; 

    [SerializeField]
    [TextArea]
    public List<string> possibleRandomTweets;

    [SerializeField]
    public List<ListStringWrapper> goodStoryLineTweets;
    [SerializeField]
    public List<ListStringWrapper> badStoryLineTweets;
    [SerializeField]
    public List<string> concreteFlaggedWords;

    public bool canUseRandomGeneralTweets = true;
    public bool isCeo = false; 

    public int tweetsPerDay = 1; 

   
}

[System.Serializable]
public struct ListStringWrapper{
    [TextArea]
    public List<string> list;
}