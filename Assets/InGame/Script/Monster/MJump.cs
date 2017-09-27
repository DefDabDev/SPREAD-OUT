using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJump : Monster
{
    [SerializeField]
    new Rigidbody2D rigidbody2D;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Tile") && !isChgColor)
        {
            rigidbody2D.velocity = new Vector2(0, 6);
            targetBlock = coll.gameObject;
            if (Mathf.Abs(coll.gameObject.transform.position.y - transform.position.y) <= 1 && !isChgColor)
            {
                _dir = _dir == DIRECTION.LEFT ? DIRECTION.RIGHT : DIRECTION.LEFT;
                transform.Rotate(new Vector3(0, 180));
            }
        }
    }
}
