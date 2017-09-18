using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TILE_TYPE
{
	FLOOR,
	CURVE_RIGHT,
	CURVE_LEFT,
}

public class Tile : MonoBehaviour {

	[SerializeField]
	private TILE_TYPE _type = TILE_TYPE.FLOOR;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag.CompareTo("Paint").Equals(0))
		{
			Paint paint = other.GetComponent<Paint>();
			if (paint.isAlreadyPainted || !gameObject.activeSelf)
			{
				other.gameObject.SetActive(false);
				return;
			}
		
			switch(_type)
			{
				case TILE_TYPE.FLOOR:
				paint.SetStickyPaint(PaintIndex.FLOOR, transform);
				break;

				case TILE_TYPE.CURVE_RIGHT:
				paint.SetStickyPaint(PaintIndex.RIGHT, transform);
				break;

				case TILE_TYPE.CURVE_LEFT:
				paint.SetStickyPaint(PaintIndex.LEFT, transform);
				break;
			}

			paint.isAlreadyPainted = true;
		}		
	}
}
