using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리펩들을 보관할 변수
    public GameObject[] prefabs;
    // 풀 담당을 하는 리스트들
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; ++i)
        {
            pools[i] = new List<GameObject>();
        }


    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // index == -1 경우, 랜덤으로 블록을 생성.
        if (index == -1)
        {
            index = Random.Range(1, 100);
            if (index < 90)
            {
                select = Instantiate(prefabs[1], transform);
                pools[1].Add(select);
            }
            else
            {
                index = Random.Range(2, prefabs.Length);
                select = Instantiate(prefabs[index], transform);
                pools[index].Add(select);
            }

            return select;
        }

        // 선택한 풀의 놀고 있는 게임 오브젝트 접근
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf && !item.CompareTag("Wall"))
            {
                // 발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                // Debug.Log(select.name);
                break;
            }
        }
        // 못찾았으면
        if (!select)
        {
            // 새롭게 생성하고 select 변수에 할당
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
