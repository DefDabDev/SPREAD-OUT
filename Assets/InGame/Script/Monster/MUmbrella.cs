using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MUmbrella : Monster
{
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Tile"))
        {
            targetBlock = coll.gameObject;
        }
    }
}
