using System.Collections;
using System.Collections.Generic;
using AL.ALUtil;
using UnityEngine;
using UnityEngine.UI;

public enum PaintIndex 
{
	BULLET,
	FLOOR,
	RIGHT,
	LEFT,
}

public class StickyPaint : MonoBehaviour {

	[SerializeField]
	private Sprite[] _images;

	[SerializeField]
	private Image _image = null;

	private void OnDisable ()
	{
		_image.fillAmount = 0f;
	}

	public void SetPaint(PaintIndex index)
	{
		gameObject.SetActive(true);
		_image.sprite = _images[(int)index];
		StartCoroutine("PaintingAnimation");
	}	

	public void SetPaintRotation(float rotation)
    {
        transform.Rotate(new Vector3(0, 0, rotation));
    }

	private IEnumerator PaintingAnimation()
	{
		float timer = 0f;
		_image.fillAmount = 0f;

		while(timer <= 1f)
		{
			timer += Time.deltaTime;
			_image.fillAmount = ALLerp.Lerp(_image.fillAmount, 1f, timer);
			yield return null;
		}
	}
}
