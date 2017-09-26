using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MTYPE
{
    MHUMAN,
    MJUMP,
    MUMBRELLA,
    MBIRD
}

// o-pool
public class MonsterManager : MonoBehaviour
{
    public static MonsterManager mm;

    [SerializeField]
    GameObject[] monsters;

    void Awake()
    {
        mm = this;
    }

    public void createMonster(MTYPE type, Vector2 pos)
    {
        GameObject obj = Instantiate(monsters[(int)type]) as GameObject;
        obj.transform.SetParent(transform);
        obj.transform.localPosition = pos;
        obj.transform.localScale = Vector2.one;
    }
}
