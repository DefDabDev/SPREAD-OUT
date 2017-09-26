using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBird : Monster
{
    void Start()
    {
        _speed *= 2;
    }

    protected override void hitPaint()
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 2;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
