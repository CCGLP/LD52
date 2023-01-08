using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverController : MonoBehaviour
{
    protected bool notCharging = true;
    void Update()
    {
        if (Time.timeSinceLevelLoad > 1 && Input.anyKeyDown && notCharging)
        {
            notCharging = false;
            SceneManager.LoadScene(0);
        }
    }
}
