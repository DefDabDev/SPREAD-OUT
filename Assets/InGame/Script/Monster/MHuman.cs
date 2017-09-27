using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MHuman : Monster
{
    bool isCurvMove = false;
    [SerializeField]
    new Rigidbody2D rigidbody2D;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Tile"))
        {
            StopCoroutine("flyingCheck");
            targetBlock = coll.gameObject;

            if (targetBlock.name.Equals("Tile_Curv(Clone)"))
            {
                if (!isCurvMove)
                    StartCoroutine("curv", -45);
                isCurvMove = true;
            }
            else
            {
                if (isCurvMove)
                {
                    isCurvMove = false;
                    StartCoroutine("curv", 45);
                }
                if (Mathf.Abs(coll.gameObject.transform.position.y - transform.position.y) <= 1 && !isChgColor)
                {
                    _dir = _dir == DIRECTION.LEFT ? DIRECTION.RIGHT : DIRECTION.LEFT;
                    transform.Rotate(new Vector3(0, 180));
                }
            }
        }
    }

    IEnumerator curv(float angle)
    {
        rigidbody2D.gravityScale = 0;
        float timer = 0f;
        Vector3 target = new Vector3(transform.localPosition.x + 85, transform.localPosition.y - 85, 0f);
        while (timer <= 1f)
        {
            timer += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(transform.localPosition, target, timer);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, -45), timer);
            yield return null;
        }

        //yield return null;
        //if (angle < 0)
        //{
        //    rigidbody2D.gravityScale = 0.2f;
        //        transform.Rotate(new Vector3(0, 0, -45));
        //    //while (transform.localRotation.z > angle)
        //    //{
        //    //    yield return null;
        //    //}
        //}
        //else
        //{
        //    rigidbody2D.gravityScale = 1;
        //    //while (transform.localRotation.z < angle)
        //    //{
        //    //    yield return null;
        //    //    transform.Rotate(new Vector3(0, 0, 5));
        //    //}
        //    transform.Rotate(new Vector3(0, 0, angle));
        //}
        ////yield return new WaitForSeconds(0.3f);
        ////transform.Rotate(new Vector3(0, 0, angle));
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Tile"))
            StartCoroutine("flyingCheck");
    }

    IEnumerator flyingCheck()
    {
        yield return new WaitForSeconds(0.4f);

        if (!isChgColor)
        {
            _dir = _dir == DIRECTION.LEFT ? DIRECTION.RIGHT : DIRECTION.LEFT;
            transform.Rotate(new Vector3(0, 180));
        }
    }
}
