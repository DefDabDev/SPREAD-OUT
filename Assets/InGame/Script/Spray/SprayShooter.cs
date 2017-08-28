using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayShooter : MonoBehaviour {

	[SerializeField]
	private Transform _firePosition;

	[SerializeField]
	private float _fireDelay = 0.1f;

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
			if (Input.GetMouseButton(0))
			{
				// fire code
				
				yield return new WaitForSeconds(_fireDelay);
			}
			yield return null;
		}
	}
}
