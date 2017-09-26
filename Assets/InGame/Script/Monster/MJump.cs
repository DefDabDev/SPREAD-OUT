using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJump : Monster
{
    [SerializeField]
    Rigidbody2D rigidbody2D;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Tile") && !isChgColor)
        {
            rigidbody2D.velocity = new Vector2(0, 4);
            targetBlock = coll.gameObject;
        }
    }
}
