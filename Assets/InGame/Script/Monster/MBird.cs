using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBird : Monster
{
    void Start()
    {
        _speed *= 2;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Tile"))
        {
            _dir = _dir == DIRECTION.LEFT ? DIRECTION.RIGHT : DIRECTION.LEFT;
            transform.Rotate(new Vector3(0, 180));
        }
    }

    protected override void hitPaint()
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 2;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine("die");
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(2);
        Destroy(this);
    }
}
