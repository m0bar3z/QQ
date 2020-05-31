using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PoolList<T> : MonoBehaviour where T : MonoBehaviour
{
    public List<T> list;
    public int spawnNumber = 10;
    public GameObject prefab;

    public virtual T GetOne()
    {
        if (list.Count > 0)
        {
            T one = list[0];
            list.RemoveAt(0);

            one.gameObject.SetActive(true);

            return one;
        }
        else
        {
            list.Add(CreateOne());
            return GetOne();
        }
    }

    public void ReturnOne(T one)
    {
        if (list.Count < spawnNumber)
        {
            list.Add(one);
            one.gameObject.SetActive(false);
        }
        else
        {
            Destroy(one);
        }
    }

    protected virtual void Start()
    {
        list = new List<T>();

        for (int i = 0; i < spawnNumber; i++)
        {
            list.Add(CreateOne());
        }
    }

    protected virtual T CreateOne()
    {
        GameObject go = Instantiate(prefab, transform);
        T t = go.GetComponent<T>();

        go.SetActive(false);

        return t;
    }
}
