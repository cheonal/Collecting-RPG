using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    [SerializeField] GameObject[] GO_Prefab;
    List<GameObject>[] pools;

    [SerializeField] private int TextObjectCount = 20;
    void Awake()
    {
        pools = new List<GameObject>[GO_Prefab.Length];
        for (int i = 0; i < GO_Prefab.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
        for (int i = 0; i < TextObjectCount; i++)
        {
            Get(0);
        }
    }
    public GameObject Get(int index)
    {
        GameObject select = null;
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        if (!select)
        {
            select = Instantiate(GO_Prefab[index], transform.position, Quaternion.identity);
            pools[index].Add(select);
        }
        return select;
    }

}
