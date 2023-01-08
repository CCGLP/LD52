using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoworkerContent : MonoBehaviour
{
    [SerializeField]
    private GameObject firedImage;
    public string coworkerhandle; 
    public void ActiveFireCoworker()
    {
        firedImage.SetActive(true); 
    }
}
