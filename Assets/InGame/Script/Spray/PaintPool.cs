using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AL.ALLog;

public class PaintPool : MonoBehaviour {

	[SerializeField]
	private PaintMover _paint;

	[SerializeField]
	private int _initPaintCount = 30;

	[SerializeField]
	private List<PaintMover> _pool;

	private void Awake () 
	{
		GeneratePaint();
	}

	private PaintMover InstantiatePaint()
	{
		PaintMover temp = Instantiate(_paint, Vector3.zero, Quaternion.identity, transform);
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

	public PaintMover GetPaint()
	{
		foreach(PaintMover paint in _pool)
		{
			if (paint.gameObject.activeSelf.Equals(false))
				return paint;
		}

		ALLog.Log("All paint on work!");

		return  InstantiatePaint();
	}
}
