using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomTweets", menuName ="LD52/RandomTweets", order = 0)]
public class RandomTweetsData : ScriptableObject
{
    [TextArea]
    public List<string> tweets;

    
}

public struct UserHandle
{
    public string userName;
    public string handle; 

    public UserHandle(string userName, string handle)
    {
        this.userName = userName;
        this.handle = handle; 
    }
}


