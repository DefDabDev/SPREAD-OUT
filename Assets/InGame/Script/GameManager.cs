using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AL.ALUtil;

public class GameManager : ALComponentSingleton<GameManager>
{
    public static bool gameEnd = false;

    int playTime = 0;
    [SerializeField]
    UnityEngine.UI.Text timeTxt;
    [SerializeField]
    UnityEngine.UI.Text resultTxt;
    [SerializeField]
    Animator anim;

    void Awake()
    {
        instance = this;
        
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! 출시전엔 제거할것
        PlayerPrefs.SetInt("score", 0);
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        //if (PlayerPrefs.GetInt("score"))
        StartCoroutine("timeCheck");
    }

    IEnumerator timeCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            playTime++;
            timeTxt.text = string.Format("{0:000} M", playTime);
        }
    }

    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    public void GameEnd()
    {
        StopCoroutine("timeCheck");
        resultTxt.text= string.Format("{0:000} M", playTime);
        anim.SetTrigger("GameEnd");
        PlayerPrefs.SetInt("score", playTime);
    }

    public void reStart()
    {
        SceneManager.LoadScene("Ready");
    }
}
