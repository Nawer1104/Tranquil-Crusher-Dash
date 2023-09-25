using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;

    public GameObject linkPrefab;

    public int links = 7;

    public Weight weight;

    public void GenerateRope()
    {
        Rigidbody2D previousRB = hook;
        for (int i = 0; i < links; i++)
        {
            GameObject link = Instantiate(linkPrefab, transform);
            HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
            joint.connectedBody = previousRB;

            if (i < links - 1)
            {
                previousRB = link.GetComponent<Rigidbody2D>();
            }
            else
            {
                weight.ConnectedRopeEnd(link.GetComponent<Rigidbody2D>());
            }
        }
    }

    public List<GameObject> GetAllChilds(GameObject Go)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < Go.transform.childCount; i++)
        {
            list.Add(Go.transform.GetChild(i).gameObject);
        }
        return list;
    }

    public void DesTroyAllChildsGameObject()
    {
        List<GameObject> childs = GetAllChilds(this.gameObject);
        for (int i = 1; i < childs.Count; i++)
        {
            Destroy(childs[i]);
        }
    }
}