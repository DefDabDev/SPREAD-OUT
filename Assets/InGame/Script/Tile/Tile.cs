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
	
	[SerializeField]
	private bool _isAlreadyPainted = false;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (_isAlreadyPainted)
		{
			other.gameObject.SetActive(false);
			return;
		}

		if (other.tag.CompareTo("Paint").Equals(0))
		{
			Paint paint = other.GetComponent<Paint>();
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

			_isAlreadyPainted = true;
		}		
	}
}
