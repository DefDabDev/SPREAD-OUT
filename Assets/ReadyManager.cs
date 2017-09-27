using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadyManager : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    public void startBT()
    {
        anim.SetTrigger("Start");
    }

    public void goInGame()
    {
        SceneManager.LoadScene("InGame");
    }
}
