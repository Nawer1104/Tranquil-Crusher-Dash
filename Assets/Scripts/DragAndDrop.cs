using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool _dragging = true;

    private Vector2 _offset;

    public static bool mouseButtonReleased;

    private Vector3 startPos;

    public GameObject target;

    public GameObject vfxSuccess;
    public GameObject vfxFail;

    private Vector3 scaleChange = new Vector3(1.5f, 1.5f, 1.5f);

    private void Awake()
    {
        startPos = transform.position;
    }

    private void OnMouseDown()
    {
        _offset = GetMousePos() - (Vector2)transform.position;
    }

    private void OnMouseDrag()
    {
        if (!_dragging) return;

        var mousePosition = GetMousePos();

        transform.position = mousePosition - _offset;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject == target)
        {
            _dragging = false;

            transform.position = target.transform.position;
            transform.localScale = scaleChange;
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;

            StartCoroutine(VFXCourutine());
        }
    }


    private void OnMouseUp()
    {
        mouseButtonReleased = true;

        if (_dragging)
        {
            transform.position = startPos;
            GameObject vfx = Instantiate(vfxFail, transform.position, Quaternion.identity);
            Destroy(vfx, .75f);
        }
    }

    private Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private IEnumerator VFXCourutine()
    {
        GameObject vfx = Instantiate(vfxSuccess, transform.position, Quaternion.identity);
        Destroy(vfx, 1f);

        yield return new WaitForSeconds(1f);

        GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObjects.Remove(gameObject);
        GameManager.Instance.CheckLevelUp();
    }
}