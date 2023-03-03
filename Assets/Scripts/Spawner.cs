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
        GameObject brick;
        for (float x = (-xSize / 2) + 0.5f; x < (xSize / 2) + 0.5f; ++x)
        {
            for (float y = (-ySize / 2) + 0.5f; y < (ySize / 2) + 0.5f; ++y)
            {
                if ((-GameManager.instance.playerPlayPointX <= x && x <= GameManager.instance.playerPlayPointX) && (-GameManager.instance.playerPlayPointY <= y && y <= GameManager.instance.playerPlayPointY))
                { continue; }
                if (upPoint[0].transform.position.x <= x && x <= upPoint[upPoint.Length - 1].transform.position.x && (GameManager.instance.playerPlayPointY + 1) <= y && y <= (upPoint[0].transform.position.y - 2)
                || upPoint[0].transform.position.x <= -x && -x <= upPoint[upPoint.Length - 1].transform.position.x && (GameManager.instance.playerPlayPointY + 1) <= -y && -y <= (upPoint[0].transform.position.y - 2))
                { brick = GameManager.instance.poolManager.GetMold(0); }
                else
                { brick = GameManager.instance.poolManager.GetMold(1); }
                brick.transform.position = new Vector3(x, y, 0);
                brick.transform.name = "(" + x + ", " + y + ")Mold";
            }
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
        int color = 0;

        switch (pos)
        {
            case 0:     //up
                size = upPoint.Length;
                points = upPoint;
                color = 0;
                break;
            case 1:     //down
                size = downPoint.Length;
                points = downPoint;
                color = 0;
                break;
            case 2:     //right
                size = rightPoint.Length;
                points = rightPoint;
                color = 1;
                break;
            case 3:     //left
                size = leftPoint.Length;
                points = leftPoint;
                color = 1;
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
            brick = GameManager.instance.poolManager.Get(-1, color);
            brick.GetComponent<Brick>().posX = x;
            brick.GetComponent<Brick>().posY = y;
            brick.transform.position = new Vector3(x, y, 0);
            brick.name = "(" + x + ", " + y + ")";
        }

    }

}
