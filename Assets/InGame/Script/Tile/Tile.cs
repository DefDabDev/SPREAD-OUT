using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TILE_TYPE
{
	FLOOR,
	WALL,
	CEILING,
	CURVE_RIGHT,
	CURVE_LEFT,
}

public enum TILE_DIRECTION
{
	UP,
	DOWN,
	RIGHT,
	LEFT
}

public class Tile : MonoBehaviour {

	[SerializeField]
	private TILE_TYPE _type = TILE_TYPE.FLOOR;

	[SerializeField]
	private TILE_DIRECTION _direction = TILE_DIRECTION.UP;

	[SerializeField]
	private bool _isAlreadyPainted = false;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag.CompareTo("Paint").Equals(0))
		{
			Paint paint = other.GetComponent<Paint>();
			if (paint.isAlreadyPainted || _isAlreadyPainted)
				return;
		
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

            switch(_direction)
            {
                case TILE_DIRECTION.LEFT:
                    paint.SetStickyPaintRot(PaintIndex.FLOOR, transform, 90);
                    break;
                case TILE_DIRECTION.RIGHT:
                    paint.SetStickyPaintRot(PaintIndex.FLOOR, transform, -90);
                    break;

            }

			paint.isAlreadyPainted = true;
			_isAlreadyPainted = true;
		}		
	}

    public void typeChange(TILE_TYPE tt)
    {
        _type = tt;
    }

    public void directionChange(TILE_DIRECTION td)
    {
        _direction = td;
    }
}
