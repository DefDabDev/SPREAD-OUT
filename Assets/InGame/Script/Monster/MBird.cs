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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.CompareTo("Paint").Equals(0))
        {
            if (isChgColor)
                return;
            
            isChgColor = true;
            hitPaint();

            StopCoroutine("walkingAnimation");
            StopCoroutine("walkingStatus");

            _image.sprite = flip[2];
            other.gameObject.SetActive(false);
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
