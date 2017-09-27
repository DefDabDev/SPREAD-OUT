using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MUmbrella : Monster
{
    bool canDirChg = true;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Tile"))
        {
            StopCoroutine("flyingCheck");
            targetBlock = coll.gameObject;
            if (Mathf.Abs(coll.gameObject.transform.position.y - transform.position.y) <= 1 && !isChgColor && canDirChg)
            {
                _dir = _dir == DIRECTION.LEFT ? DIRECTION.RIGHT : DIRECTION.LEFT;
                transform.Rotate(new Vector3(0, 180));

                canDirChg = false;
                StartCoroutine("dirChgDelay");
            }
        }
    }

    IEnumerator dirChgDelay()
    {
        yield return new WaitForSeconds(0.3f);

        canDirChg = true;
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
