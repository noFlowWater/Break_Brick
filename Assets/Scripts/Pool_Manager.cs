using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool_Manager : MonoBehaviour
{
    //프리렙들을 보관할 변수들 
    public GameObject[] prefabs;

    //어떤 프리팹의 풀 담당 리스트들
    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for(int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        } 
    }

    public GameObject Get(int index)
    {
        GameObject select = null;
        
        foreach(GameObject ball in pools[index])
        {
            if (!ball.activeSelf)
            {
                select = ball;
                select.SetActive(true);
                break;
            }
        }
        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
