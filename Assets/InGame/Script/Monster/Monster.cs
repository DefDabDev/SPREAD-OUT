using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private const float SPEED = 3f;
    private float _speed = SPEED;
    private bool isChgColor = false;    // 물감에게 맞았는지

    [Header("Animation")]
    [SerializeField]
    UnityEngine.UI.Image _image;
    [SerializeField]
    Sprite[] flip;
    /* Animation flip 규칙
     *   [0] 걷기_0
     *   [1] 걷기_1
     *   [2] 멈춤
     *   [3] 맞음
     *   [4] 숙임
     *   [5] 점프_0
     *   [6] 점프_1
     */


    void Start()
    {
        StartCoroutine("sleepStatus");
    }

    IEnumerator sleepStatus()
    {
        Vector3 screenPoint;
        while (true)
        {
            screenPoint = Camera.main.WorldToViewportPoint(transform.position);
            if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
            {
                StartCoroutine("walkingStatus");
                StartCoroutine("walkingAnimation");
                StopCoroutine("sleepStatus");
            }
            yield return null;
        }
    }

    IEnumerator walkingStatus()
    {
        while (true)
        {
            yield return null;
            transform.position -= new Vector3(_speed * Time.deltaTime, 0);
        }
    }

    IEnumerator walkingAnimation()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            _image.sprite = flip[0];
            yield return new WaitForSeconds(0.3f);
            _image.sprite = flip[1];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.CompareTo("Paint").Equals(0))
        {
            // Paint paint = other.GetComponent<Paint>();
            if (isChgColor)
                return;

            // 헬멧색 바뀜 !
            isChgColor = true;
            StopCoroutine("walkingAnimation");
            StopCoroutine("walkingStatus");
            _image.sprite = flip[2];
        }
        else if (other.tag.CompareTo("Attack").Equals(0))
        {
            // 공격 맞음 !
            if (isChgColor)
            {
                _image.sprite = flip[3];
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine("hideOnBush");
            }
        }
    }
        
    IEnumerator hideOnBush()
    {
        float originPos = gameObject.transform.localPosition.y;

        yield return new WaitForSeconds(0.5f);
        _image.sprite = flip[4];

        while ((originPos - 135) < gameObject.transform.localPosition.y)
        {
            yield return null;
            gameObject.transform.localPosition -= new Vector3(0, 100 * Time.deltaTime);
        }
    }
}
