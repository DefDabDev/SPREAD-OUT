using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroAnimation : MonoBehaviour {

	[SerializeField]
	private string _sceneName = string.Empty;

	[SerializeField]
	private float _delay = 0.1f;

	private Text _text = null;

	private void Awake ()
	{
		_text = GetComponent<Text>();
		StartCoroutine("Animation");
	}

	private IEnumerator Animation()
	{
		StringBuilder builder = new StringBuilder();
		builder.Append("Project by");
		_text.text = builder.ToString();
		yield return new WaitForSeconds(_delay);

		builder.AppendLine();
		builder.Append(":D");
		_text.text = builder.ToString();
		yield return new WaitForSeconds(_delay);

		builder.Append(":D");
		_text.text = builder.ToString();
		yield return new WaitForSeconds(_delay);

		builder.Append(":D");
		_text.text = builder.ToString();
		yield return new WaitForSeconds(_delay);

		builder.AppendLine();
		builder.Append("<size=50>Copyright(c)2017 Team :D:D:D All rights reserved.</size>");
		_text.text = builder.ToString();
		yield return new WaitForSeconds(_delay * 2);

		SceneManager.LoadScene(_sceneName);
	}
}
