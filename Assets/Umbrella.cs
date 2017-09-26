using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{
    public int _hp = 5;

    [SerializeField]
    UnityEngine.UI.Image _image;
    [SerializeField]
    Monster m;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.CompareTo("Paint").Equals(0))
        {
            _hp--;
            other.gameObject.SetActive(false);
            
            if (_hp.Equals(0))
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine("remove");
            }
        }
    }

    /// <summary>
    /// Umbrella Remove
    /// </summary>
    /// <desc>
    /// Opacity 255 -> to 0
    /// posY--
    /// </desc>
    /// <returns></returns>
    IEnumerator remove()
    {
        yield return new WaitForSeconds(0.1f);
        m._type = MTYPE.MHUMAN;

        while (_image.color.a > 0)
        {
            yield return new WaitForSeconds(0.05f);
            _image.color -= new Color(0, 0, 0, 0.1f);
            transform.position -= new Vector3(0, 5 * Time.deltaTime);
            transform.Rotate(new Vector3(0, 0, -3));
        }
    }
}
