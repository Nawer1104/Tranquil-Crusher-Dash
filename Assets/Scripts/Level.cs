using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<GameObject> gameObjects;

    // private void Awake()
    // {
    //     AddComponents();
    // }

    // public void AddComponents()
    // {
    //     foreach (GameObject gameObject in GetAllChilds(gameObject))
    //     {
    //         if (!gameObject.activeSelf)
    //         {
    //             gameObject.SetActive(true);
    //         }
    //     }

    //     AddEnemy();
    // }

    // public void AddEnemy()
    // {
    //     enemies.Clear();

    //     foreach (Enemy enemy in GetComponentsInChildren<Enemy>())
    //     {
    //         enemies.Add(enemy);
    //     }
    // }

    public static List<GameObject> GetAllChilds(GameObject Go)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < Go.transform.childCount; i++)
        {
            list.Add(Go.transform.GetChild(i).gameObject);
        }
        return list;
    }
}
