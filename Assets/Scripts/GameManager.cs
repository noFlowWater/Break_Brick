using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public MainCamera mainCamera;
    public PoolManager poolManager;
    // public GameObject[] VerticalChecker;
    // public GameObject[] HorizontalChecker;
    public Spawner spawner;


    public int level;
    public int score;
    public int ballNumber;
    public int life;
    public int durability;
    public float fireTime;
    public float ballSpeed;

    public bool isPlayerTurn;

    public float playerPlayPointX;
    public float playerPlayPointY;

    public float delayRate;
    public float delay;
    public float brickSpeed;

    public float timeScale;
    public int funcCount;
    bool isHSpawn;

    private void Awake()
    {
        instance = this;
        score = 0;
        Time.timeScale = timeScale;
        // LineBreakCheck();

    }

    void Update()
    {
        Time.timeScale = timeScale;
    }


    public void LineBreakCheck()
    {
        isHSpawn = false;
        delay = -delayRate;
        HorizontalLineBreakCheck(1);
        delay = -delayRate;
        HorizontalLineBreakCheck(-1);
        delay = -delayRate;
        VerticalLineBreakCheck(1);
        delay = -delayRate;
        VerticalLineBreakCheck(-1);

    }

    void HorizontalLineBreakCheck(int dir)
    {
        float startXPos = spawner.upPoint[spawner.upPoint.Length - 1].transform.position.x;
        float endXPos = spawner.upPoint[0].transform.position.x;
        float startYPos = dir * (playerPlayPointY + 1);
        float endYPos = dir * spawner.upPoint[0].transform.position.y;

        // canCheck = true;
        // Debug.Log("startXPos: " + startXPos);
        // Debug.Log("endXPos: " + endXPos);
        // Debug.Log("startYPos: " + startYPos);
        // Debug.Log("endYPos: " + endYPos);
        // Debug.Log("dir: " + dir + "hor: " + endYPos);

        // 수평라인의 블록이 모두 사라졌는지를 체크한다. 플레이어 화면에 존재하는 블록만 체크한다.
        for (float y = startYPos; Mathf.Abs(y) < Mathf.Abs(endYPos) - 1; y += dir)
        {

            float x = startXPos;
            // Debug.Log("y " + y);
            // Debug.Log("abs(y) " + Mathf.Abs(y));
            // Debug.Log("endY " + endYPos);
            // Debug.Log("abs(endY) " + Mathf.Abs(endYPos));
            bool needFill = true;
            for (; x <= endXPos; ++x)
            {
                GameObject brick = GameObject.Find("(" + x + ", " + y + ")");
                if (brick == null) { brick = GameObject.Find("(" + x + ", " + y + ")Move"); }
                if (brick != null) { needFill = false; break; }
                // Debug.Log(x + "," + y);
            }
            if (needFill)
            {
                Debug.Log("테트리스!");

                HorizontalLineMove(dir, y);
            };

        }

    }
    void HorizontalLineMove(int dir, float needY)
    {
        float startXPos = spawner.upPoint[spawner.upPoint.Length - 1].transform.position.x;
        float endXPos = spawner.upPoint[0].transform.position.x;
        float endYPos = dir * spawner.upPoint[0].transform.position.y;
        float x;
        float y = needY;

        GameObject brick = null;
        // Debug.Log(y);

        // 안전장치?
        for (x = startXPos * dir; Mathf.Abs(x) <= Mathf.Abs(endXPos); x += dir)
        {
            brick = GameObject.Find("(" + x + ", " + needY + ")");
            if (brick == null) { brick = GameObject.Find("(" + x + ", " + needY + ")Move"); }
            if (brick != null)
            {
                Debug.Log("gaesibal");
                return;
            }
        }
        isHSpawn = true;

        // 옮길 수 있는 라인을 찾음.
        while (brick == null)
        {
            // Debug.Log(1);
            x = startXPos;
            bool next = true;
            for (; x <= endXPos; ++x)
            {
                brick = GameObject.Find("(" + x + ", " + y + ")");
                if (brick == null) { brick = GameObject.Find("(" + x + ", " + y + ")Move"); }
                if (brick != null)
                {
                    next = false;
                    break;
                }

            }
            if (!next) { break; }
            else { y += dir; }
            if ((Mathf.Abs(y) > Mathf.Abs(endYPos)))
            {
                Debug.Log("1. 무한루프");
                break;
            }
        }
        // delay += delayRate;
        // 하나하나 옮긴다.
        for (x = startXPos * dir; Mathf.Abs(x) <= Mathf.Abs(endXPos); x += dir)
        {
            brick = GameObject.Find("(" + x + ", " + y + ")");
            if (brick == null) { brick = GameObject.Find("(" + x + ", " + y + ")Move"); }
            if (brick != null)
            {
                brick.transform.name = "(" + x + ", " + needY + ")Move";
                delay += delayRate;
                StartCoroutine(MoveBrick(brick, x, needY));
            }
        }
        if (y == endYPos)
        {
            if (dir == 1) { spawner.Spawn(0); }
            else { spawner.Spawn(1); }
        }



    }
    void VerticalLineBreakCheck(int dir)
    {
        float startXPos = dir * (playerPlayPointX + 1);
        float endXPos = dir * spawner.rightPoint[0].transform.position.x;
        float startYPos = spawner.rightPoint[spawner.rightPoint.Length - 1].transform.position.y;
        float endYPos = spawner.rightPoint[0].transform.position.y;
        // float startXPos = dir * (playerPlayPointX + 1);
        // float endXPos = dir * spawner.rightPoint[0].transform.position.x;
        // float startYPos = spawner.rightPoint[spawner.rightPoint.Length-1].transform.position.y;
        // float 
        // Debug.Log(startYPos);
        // 수직라인의 블록이 모두 사라졌는지를 체크한다. 플레이어 화면에 존재하는 블록만 체크한다.
        for (float x = startXPos; Mathf.Abs(x) < Mathf.Abs(endXPos) - 1; x += dir)
        {

            float y = startYPos;
            bool needFill = true;
            for (; y <= endYPos; ++y)
            {
                GameObject brick = GameObject.Find("(" + x + ", " + y + ")");
                if (brick != null) { needFill = false; break; }
                // Debug.Log(x + "," + y);
            }
            if (needFill)
            {
                Debug.Log("테트리스!");
                VerticalLineMove(dir, x);
            };

        }
    }
    void VerticalLineMove(int dir, float needX)
    {
        float startYPos;
        float endYPos;
        if (!isHSpawn)
        {
            startYPos = spawner.rightPoint[spawner.rightPoint.Length - 1].transform.position.y;
            endYPos = spawner.rightPoint[0].transform.position.y;
            // Debug.Log(false);
        }
        else
        {
            startYPos = -playerPlayPointY;
            endYPos = playerPlayPointY;
            // Debug.Log(true);
        }
        // Debug.Log(startYPos);
        // Debug.Log(endYPos);
        // startYPos = spawner.rightPoint[spawner.rightPoint.Length - 1].transform.position.y;
        // endYPos = spawner.rightPoint[0].transform.position.y;
        float endXPos = dir * spawner.rightPoint[0].transform.position.x;
        float x = needX;
        float y;

        GameObject brick = null;
        // 안전장치?
        for (y = startYPos * dir; Mathf.Abs(y) <= Mathf.Abs(endYPos); y += dir)
        {
            brick = GameObject.Find("(" + needX + ", " + y + ")");
            if (brick != null)
            {
                Debug.Log("gaesibal2");
                return;
            }
        }

        // Debug.Log(y);
        while (brick == null && (Mathf.Abs(x) <= Mathf.Abs(endXPos)))
        {
            // Debug.Log(1);
            y = startYPos;
            bool next = true;
            for (; y <= endYPos; ++y)
            {
                brick = GameObject.Find("(" + x + ", " + y + ")");
                if (brick != null)
                {
                    next = false;
                    break;
                }

            }
            if (!next) { break; }
            else { x += dir; }
        }
        // delay += delayRate;
        for (y = startYPos * dir; Mathf.Abs(y) <= Mathf.Abs(endYPos); y += dir)
        {
            brick = GameObject.Find("(" + x + ", " + y + ")");
            if (brick != null)
            {
                brick.transform.name = "(" + needX + ", " + y + ")";
                delay += delayRate;
                StartCoroutine(MoveBrick(brick, needX, y));


            }
        }
        if (x == endXPos)
        {
            if (dir == 1) { spawner.Spawn(2); }
            else { spawner.Spawn(3); }
        }
    }

    public IEnumerator MoveBrick(GameObject brick, float x, float y)
    {
        ++funcCount;
        yield return new WaitForSeconds(.1f * delay);
        // Vector3 target = new Vector3(x, y, 0);
        // brick.transform.position = target;

        brick.GetComponent<Brick>().posX = x;
        brick.GetComponent<Brick>().posY = y;

        --funcCount;
    }

}
