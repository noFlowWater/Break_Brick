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
            if ((x >= -GameManager.instance.playerPlayPointX && x <= GameManager.instance.playerPlayPointX) && (y >= -GameManager.instance.playerPlayPointY && y <= GameManager.instance.playerPlayPointY)) { continue; }
            brick = GameManager.instance.poolManager.Get(-1);
            brick.GetComponent<Brick>().posX = x;
            brick.GetComponent<Brick>().posY = y;
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
            case 2:     //right
                size = rightPoint.Length;
                points = rightPoint;
                break;
            case 3:     //left
                size = leftPoint.Length;
                points = leftPoint;
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
            brick = GameObject.Find("(" + x + ", " + y + ")");
            if (brick != null) { continue; }
            brick = GameManager.instance.poolManager.Get(-1);
            brick.GetComponent<Brick>().posX = x;
            brick.GetComponent<Brick>().posY = y;
            brick.transform.position = new Vector3(x, y, 0);
            brick.name = "(" + x + ", " + y + ")";
        }

    }

}
