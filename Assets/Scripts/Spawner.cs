using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pointObject;

    GameObject[] points;
    int xSize, ySize, size;

    // 중앙을 기준으로 (-xSize,-ySize) ~ (xSize,ySize)까지 스폰 포인트를 생성해서 블록을 생성한다.
    // 가로 xSize * 2개, 세로 ySize * 4개 총 ((2 * xSize) + 1) * ((4 * ySize) + 1)개를 최대 생성할 수 있다.
    private void Awake()
    {
        GameObject point;
        xSize = 8;
        ySize = 6;
        size = ((2 * xSize) + 1) * ((4 * ySize) + 1);
        float x, y = -ySize - 0.5f;

        // 스폰 포인트를 각 위치에 생성 및 위치시킴.
        points = new GameObject[size];
        for (int i = 0; i < size; ++i)
        {
            point = Instantiate(pointObject, transform);
            points[i] = point;
            x = (i % ((2 * xSize) + 1)) - xSize;
            if (x == -xSize) { y += 0.5f; }
            point.transform.position = new Vector3(x, y, 0);
            point.transform.name = "Point(" + x + "," + y + ")";

        }

        Spawn();

    }

    public void Spawn()
    {
        GameObject gameOb;
        for (int i = 0; i < size; ++i)
        {
            float x = points[i].transform.position.x;
            float y = points[i].transform.position.y;

            if (GameObject.Find("PoolManager").transform.Find("(" + x + "," + y + ")") != null) { continue; }
            if ((x > -4 && x < 4) && (y > -2 && y < 2)) { continue; }

            float randCreate = Random.Range(0, 10000) / 100;
            float probability = 5;
            switch (randCreate)
            {
                case float f when (f < probability):
                    gameOb = GameManager.instance.poolManager.Get(4);
                    break;

                case float f when (f >= probability && f < 2 * probability):
                    gameOb = GameManager.instance.poolManager.Get(2);
                    break;

                case float f when (f >= 2 * probability && f < 3 * probability):
                    gameOb = GameManager.instance.poolManager.Get(3);
                    break;

                default:
                    gameOb = GameManager.instance.poolManager.Get(1);
                    break;
            }





            gameOb.transform.position = new Vector3(x, y, 0);
            gameOb.transform.name = "(" + x + "," + y + ")";
        }
    }
}
