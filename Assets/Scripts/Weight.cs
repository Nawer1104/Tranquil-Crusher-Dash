using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weight : MonoBehaviour
{
    public float distanceFromChainEnd = 0.6f;

    public GameObject vfx;

    public GameObject vfxDestroy;

    public Rope rope;

    public int Id;

    private Vector2 startPos;

    private bool canCollide = true;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        startPos = transform.position;

        rb.gravityScale = 0;

    }

    private void OnMouseDown()
    {
        rope.GenerateRope();
        rb.gravityScale = 1;
    }

    public void ConnectedRopeEnd(Rigidbody2D endRB)
    {
        HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedBody = endRB;
        joint.anchor = Vector2.zero;
        joint.connectedAnchor = new Vector2(0f, -distanceFromChainEnd);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canCollide) return;

        if (collision.gameObject.tag == "Target")
        {
            canCollide = false;
            GameObject vfxCollide = Instantiate(vfxDestroy, transform.position, transform.rotation) as GameObject;
            Destroy(vfxCollide, 1f);
            GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObjects.Remove(collision.gameObject);
            GameManager.Instance.CheckLevelUp();
            collision.gameObject.SetActive(false);
        }
    }

    public void ResetPos()
    {
        transform.position = startPos;
        canCollide = true;
        rope.GenerateRope();
    }

    public void DesTroyGameObject()
    {
        GameObject vfxDes = Instantiate(vfxDestroy, transform.position, transform.rotation);
        Destroy(vfxDes, 1f);
        Destroy(gameObject);
    }
}
