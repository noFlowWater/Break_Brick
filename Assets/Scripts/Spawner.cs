using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    // 생성 포인트
    public GameObject[] upPoint;
    public GameObject[] rightPoint;
    public GameObject[] downPoint;
    public GameObject[] leftPoint;


    public int xSize;
    public int ySize;

    private void Awake()
    {

        InitSpawn();
    }

    private void InitSpawn()
    {
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
