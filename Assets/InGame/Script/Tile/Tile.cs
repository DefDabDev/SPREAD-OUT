﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TILE_TYPE
{
	FLOOR,
	WALL,
	CEILING,
	CURVE_RIGHT,
	CURVE_LEFT,
	CURVE_RIGHT2,
	CURVE_LEFT2,
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
	private BoxCollider2D _collider = null;

	[SerializeField]
	private StickyPaint _stickyPaint;

	[SerializeField]
	private bool _isAlreadyPainted = false;

	private readonly Vector2 _originTileSize = new Vector2(250f, 250f);
	private void OnDisable()
	{
		_isAlreadyPainted = false;
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0f);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag.CompareTo("Paint").Equals(0))
		{
			if (_isAlreadyPainted)
			{
				other.gameObject.SetActive(false);
				return;
			}
            painting();
			other.gameObject.SetActive(false);
		}		
	}

    public void painting()
    {
        switch (_type)
        {
            case TILE_TYPE.FLOOR:
                _stickyPaint.SetPaint(PaintIndex.FLOOR);
                break;

            case TILE_TYPE.CURVE_RIGHT:
                _stickyPaint.SetPaint(PaintIndex.RIGHT);
                break;

            case TILE_TYPE.CURVE_LEFT:
                _stickyPaint.SetPaint(PaintIndex.LEFT);
                break;

			case TILE_TYPE.CURVE_RIGHT2:
				_stickyPaint.SetPaint(PaintIndex.RIGHT2);
				break;

			case TILE_TYPE.CURVE_LEFT2:
				_stickyPaint.SetPaint(PaintIndex.LEFT2);
				break;
        }

        switch (_direction)
        {
            case TILE_DIRECTION.LEFT:
                _stickyPaint.SetPaintRotation(90f);
                break;
            case TILE_DIRECTION.RIGHT:
                _stickyPaint.SetPaintRotation(-90f);
                break;

        }
        _isAlreadyPainted = true;
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 1f);
    }

    public void typeChange(TILE_TYPE type)
    {
        _type = type;

		switch(_type)
		{
			case TILE_TYPE.CURVE_RIGHT: case TILE_TYPE.CURVE_RIGHT2:
				_collider.isTrigger = true;
				gameObject.tag = "CurvTile";
				_collider.size = new Vector2(_originTileSize.x + 10, _originTileSize.y + 10);
				break;

			default:
				_collider.isTrigger = false;
				gameObject.tag = "Tile";
				_collider.size = _originTileSize;
				break;
		}
    }

    public void directionChange(TILE_DIRECTION direction)
    {
        _direction = direction;
    }
}
