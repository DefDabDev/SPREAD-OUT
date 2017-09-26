using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayShooter : MonoBehaviour {

	[SerializeField]
	private PaintPool _pool;

	[SerializeField]
	private ParticleSystem _fireParticle;

	[SerializeField]
	private float _fireDelay = 0.1f;

	[SerializeField]
	private bool _isLock = false;
	public bool isLock {set {_isLock = value;} get {return _isLock;}}

	private SprayRotator _rotator = null;

	private void Start () 
	{
		_rotator = GetComponent<SprayRotator>();
		StartCoroutine("FireSpray");
	}
	
	private IEnumerator FireSpray()
	{
		while(true)
		{
			if (!_isLock && Input.GetMouseButton(0))
			{
				SetPaint();
				yield return new WaitForSeconds(_fireDelay);
			}
			yield return null;
		}
	}

	private void SetPaint()
	{
		Paint temp = _pool.GetPaint();
		temp.SetPaint(_rotator.currentAngle, _fireParticle.transform.position);
		 _fireParticle.Emit(3);
		// if (!_fireParticle.isPlaying)
		// 	_fireParticle.Play();
	}
}
