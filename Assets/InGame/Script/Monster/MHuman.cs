using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MHuman : Monster
{
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Tile"))
            StopCoroutine("flyingCheck");
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Tile"))
            StartCoroutine("flyingCheck");
    }

    IEnumerator flyingCheck()
    {
        yield return new WaitForSeconds(0.3f);

        if (!isChgColor)
        {
            _dir = _dir == DIRECTION.LEFT ? DIRECTION.RIGHT : DIRECTION.LEFT;
            transform.Rotate(new Vector3(0, 180));
        }
    }
}
