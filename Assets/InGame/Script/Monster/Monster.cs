using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DIRECTION
{
    LEFT,
    RIGHT
}


public class Monster : MonoBehaviour
{
    private const float SPEED = 1.8f;
    protected float _speed = SPEED;
    protected bool isChgColor = false;    // 물감에게 맞았는지

    protected DIRECTION _dir = DIRECTION.LEFT;
    public MTYPE _type = MTYPE.MHUMAN;

    protected GameObject targetBlock;
    [SerializeField]
    GameObject helmet;

    [Header("Animation")]
    [SerializeField]
    protected UnityEngine.UI.Image _image;
    [SerializeField]
    protected Sprite[] flip;
    /* Animation flip 규칙
     *   [0] 걷기_0
     *   [1] 걷기_1
     *   [2] 멈춤
     *   [3] 맞음
     *   [4] 숙임
     *   [5] 점프
     */


    void Awake()
    {
        targetBlock = null;
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
            if (_dir.Equals(DIRECTION.LEFT))
                transform.position -= new Vector3(_speed * Time.deltaTime, 0);
            else
                transform.position += new Vector3(_speed * Time.deltaTime, 0);
        }
    }

    IEnumerator walkingAnimation()
    {
        if (flip == null || flip[0] == null || flip[1] == null)
            yield break;

        while (true)
        {
            yield return new WaitForSeconds(0.07f);
            _image.sprite = flip[0];
            yield return new WaitForSeconds(0.07f);
            _image.sprite = flip[1];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.CompareTo("Paint").Equals(0))
        {
            if (isChgColor || _type.Equals(MTYPE.MUMBRELLA))
                return;

            // 헬멧색 바뀜 !
            if (!helmet.Equals(null))
                helmet.SetActive(true);
            isChgColor = true;
            hitPaint();

            StopCoroutine("walkingAnimation");
            StopCoroutine("walkingStatus");

            _image.sprite = flip[2];
            other.gameObject.SetActive(false);
        }
        else if (other.tag.CompareTo("Attack").Equals(0))
        {
            // 공격 맞음 !
            if (isChgColor)
            {
                if (!helmet.Equals(null))
                    helmet.SetActive(false);
                _image.sprite = flip[3];
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine("hideOnBush");

                targetBlock.SendMessage("painting");
            }
        }
    }

    protected virtual void hitPaint()
    {
    }

    IEnumerator hideOnBush()
    {
        float originPos = gameObject.transform.localPosition.y;

        yield return new WaitForSeconds(0.5f);
        _image.sprite = flip[4];

        gameObject.GetComponent<Rigidbody2D>().gravityScale = 2f;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(2f);

        Destroy(this);
    }
}
