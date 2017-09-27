using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int playTime = 0;
    [SerializeField]
    UnityEngine.UI.Text timeTxt;

    void Awake()
    {
        PlayerPrefs.SetInt("score", 0);
        //if (PlayerPrefs.GetInt("score"))
        StartCoroutine("timeCheck");
    }

    IEnumerator timeCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            playTime++;
            timeTxt.text = string.Format("{0:000} M", (int)playTime);
        }
    }
}
