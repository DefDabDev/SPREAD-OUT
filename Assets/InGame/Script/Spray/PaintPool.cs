using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AL.ALLog;

public class PaintPool : MonoBehaviour {

	[SerializeField]
	private Paint _paint;

	[SerializeField]
	private int _initPaintCount = 30;

	[SerializeField]
	private List<Paint> _pool;

	private void Awake () 
	{
		GeneratePaint();
	}

	private Paint InstantiatePaint()
	{
		Paint temp = Instantiate(_paint, Vector3.zero, Quaternion.identity, transform);
		temp.name = _paint.name;
		temp.gameObject.SetActive(false);
		_pool.Add(temp);
		return temp;
	}

	private void GeneratePaint()
	{
		for (int i = 0; i < _initPaintCount; ++i)
		{
			InstantiatePaint();
		}
	}

	public Paint GetPaint()
	{
		foreach(Paint paint in _pool)
		{
			if (paint.gameObject.activeSelf.Equals(false))
				return paint;
		}

		ALLog.Log("All paint on work!");

		return  InstantiatePaint();
	}
}
