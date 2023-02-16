using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    // 생성 포인트
    public GameObject[] leftPoint;
    public GameObject[] rightPoint;
    public GameObject[] upPoint;
    public GameObject[] downPoint;


    int xSize;
    int ySize;

    private void Awake()
    {
        xSize = 20;
        ySize = 12;
        InitSpawn();
    }

    private void InitSpawn()
    {
        GameObject brick;

        float x = -(xSize / 2) - 0.5f; ;
        float y = -(ySize / 2) - 0.5f;


        // (-9.5, -5.5) ~ (9.5, 5.5) 에 블록 생성
        for (int i = 0; i < xSize * ySize; ++i)
        {
            if (x == 9.5) { x = -x; }
            else { ++x; }
            if (x == -9.5f) { y += 1; }
            // (-2.5, -1.5) ~ (2.5, 1.5)는 시작스폰시 제외
            if ((x >= -2.5f && x <= 2.5f) && (y >= -1.5f && y <= 1.5f)) { continue; }
            brick = GameManager.instance.poolManager.Get(-1);
            brick.transform.position = new Vector3(x, y, 0);
            brick.name = "(" + x + ", " + y + ")";

        }
        for (int i = 0; i < 4; ++i)
        {
            Spawn(i);
        }
    }

    public void Spawn(int pos)
    {
        // pos: 0(up), 1(down), 2(left), 3(right)
        int size = -1;
        GameObject[] points = null;
        GameObject brick;

        switch (pos)
        {
            case 0:     //up
                size = upPoint.Length;
                points = upPoint;
                break;
            case 1:     //down
                size = downPoint.Length;
                points = downPoint;
                break;
            case 2:     //left
                size = leftPoint.Length;
                points = leftPoint;
                break;

            case 3:     //right
                size = rightPoint.Length;
                points = rightPoint;
                break;
            default:
                Debug.Log("Spawn의 인자가 잘못되었습니다.");
                break;

        }
        if (points == null) { return; }

        for (int i = 0; i < size; ++i)
        {
            float x = points[i].transform.position.x;
            float y = points[i].transform.position.y;
            brick = GameManager.instance.poolManager.Get(-1);
            brick.transform.position = new Vector3(x, y, 0);
            brick.name = "(" + x + ", " + y + ")";
        }

    }

    // public GameObject pointObject;

    // GameObject[] points;
    // int xSize, ySize, size;

    // // 중앙을 기준으로 (-xSize,-ySize) ~ (xSize,ySize)까지 스폰 포인트를 생성해서 블록을 생성한다.
    // // 가로 xSize * 2개, 세로 ySize * 4개 총 ((2 * xSize) + 1) * ((4 * ySize) + 1)개를 최대 생성할 수 있다.
    // private void Awake()
    // {
    //     GameObject point;
    //     xSize = 8;
    //     ySize = 8;
    //     size = ((2 * xSize) + 1) * ((2 * ySize) + 1);
    //     float x, y = -ySize - 1;

    //     // 스폰 포인트를 각 위치에 생성 및 위치시킴.
    //     points = new GameObject[size];
    //     for (int i = 0; i < size; ++i)
    //     {
    //         point = Instantiate(pointObject, transform);
    //         points[i] = point;
    //         x = (i % ((2 * xSize) + 1)) - xSize;
    //         if (x == -xSize) { y += 1; }
    //         point.transform.position = new Vector3(x, y, 0);
    //         point.transform.name = "Point(" + x + "," + y + ")";

    //     }

    //     Spawn();

    // }

    // public void Spawn()
    // {
    //     GameObject gameOb;
    //     for (int i = 0; i < size; ++i)
    //     {
    //         float x = points[i].transform.position.x;
    //         float y = points[i].transform.position.y;

    //         if (GameObject.Find("PoolManager").transform.Find("(" + x + "," + y + ")") != null) { continue; }
    //         if ((x > -4 && x < 4) && (y > -2 && y < 2)) { continue; }

    //         float randCreate = Random.Range(0, 10000) / 100;
    //         float probability = 5;
    //         switch (randCreate)
    //         {
    //             case float f when (f < probability):
    //                 gameOb = GameManager.instance.poolManager.Get(4);
    //                 break;

    //             case float f when (f >= probability && f < 2 * probability):
    //                 gameOb = GameManager.instance.poolManager.Get(2);
    //                 break;

    //             case float f when (f >= 2 * probability && f < 3 * probability):
    //                 gameOb = GameManager.instance.poolManager.Get(3);
    //                 break;

    //             default:
    //                 gameOb = GameManager.instance.poolManager.Get(1);
    //                 break;
    //         }





    //         gameOb.transform.position = new Vector3(x, y, 0);
    //         gameOb.transform.name = "(" + x + "," + y + ")";
    //     }
    // }


}
