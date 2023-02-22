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


    public float level;
    public int score;
    public int ballNumber;
    public int life;
    public int durability;

    public float ballSpeed;

    public bool isPlayerTurn;

    public float playerPlayPointX;
    public float playerPlayPointY;

    public float delayRate;
    public float delay;
    public float brickSpeed;

    public float timeScale;
    public int funcCount;


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
        float startXPos = dir * spawner.upPoint[0].transform.position.x;
        float endXpos = dir * spawner.upPoint[spawner.upPoint.Length - 1].transform.position.x;
        float startYPos = dir * (playerPlayPointY + 1);
        float endYPos = dir * (spawner.upPoint[0].transform.position.y - 2);

        for (float y = startYPos; dir * y <= dir * endYPos; y += dir)
        {
            bool needFill = true;
            for (float x = startXPos; dir * x <= dir * endXpos; x += dir)
            {

                GameObject brick = GameObject.Find("(" + x + ", " + y + ")");
                if (brick != null) { needFill = false; break; }
            }
            if (needFill)
            {
                Debug.Log("테트리스!H");
                HorizontalLineMove(dir, y);
            }
        }
    }
    void HorizontalLineMove(int dir, float needY)
    {
        float startXPos = dir * spawner.upPoint[0].transform.position.x;
        float endXPos = dir * spawner.upPoint[spawner.upPoint.Length - 1].transform.position.x;
        float endYPos = dir * spawner.upPoint[0].transform.position.y;
        float x;
        float y;

        GameObject brick = null;

        Debug.Log("startXPos: " + startXPos);
        Debug.Log("endXPos: " + endXPos);
        Debug.Log("endYPos: " + endYPos);
        Debug.Log("needY: " + needY);


        for (y = needY; dir * y < dir * endYPos; y += dir)
        {
            bool next = true;
            for (x = startXPos; dir * x <= dir * endXPos; x += dir)
            {
                brick = GameObject.Find("(" + x + ", " + y + ")");
                if (brick != null)
                {
                    next = false;
                    break;
                }

            }
            if (!next) { break; }
        }
        Debug.Log("y: " + y);
        // delay += delayRate;
        for (x = startXPos; dir * x <= dir * endXPos; x += dir)
        {
            brick = GameObject.Find("(" + x + ", " + y + ")");
            if (brick != null)
            {
                brick.transform.name = "(" + x + ", " + needY + ")";
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
        float endXPos = dir * (spawner.rightPoint[0].transform.position.x - 2);
        float startYPos = dir * spawner.rightPoint[0].transform.position.y;
        float endYPos = dir * spawner.rightPoint[spawner.rightPoint.Length - 1].transform.position.y;

        for (float x = startXPos; dir * x <= dir * endXPos; x += dir)
        {

            bool needFill = true;
            for (float y = startYPos; dir * y >= dir * endYPos; y -= dir)
            {

                GameObject brick = GameObject.Find("(" + x + ", " + y + ")");
                if (brick != null) { needFill = false; break; }
            }
            if (needFill)
            {
                // Debug.Log("테트리스!V");
                VerticalLineMove(dir, x);
            }
        }
    }
    void VerticalLineMove(int dir, float needX)
    {
        float startYPos = dir * spawner.rightPoint[0].transform.position.y;
        float endYPos = dir * spawner.rightPoint[spawner.rightPoint.Length - 1].transform.position.y;
        float endXPos = dir * spawner.rightPoint[0].transform.position.x;
        float x;
        float y;

        GameObject brick = null;

        // Debug.Log("startYPos: " + startYPos);
        // Debug.Log("endYPos: " + endYPos);
        // Debug.Log("endXPos: " + endXPos);
        // Debug.Log("needX: " + needX);

        for (x = needX; dir * x < dir * endXPos; x += dir)
        {
            bool next = true;
            for (y = startYPos; dir * y >= dir * endYPos; y -= dir)
            {
                brick = GameObject.Find("(" + x + ", " + y + ")");
                if (brick != null)
                {
                    next = false;
                    break;
                }

            }
            if (!next) { break; }
        }
        // Debug.Log("x: " + x);

        // delay += delayRate;
        for (y = startYPos; dir * y >= dir * endYPos; y -= dir)
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

        brick.GetComponent<Brick>().isMove = true;
        brick.GetComponent<Brick>().posX = x;
        brick.GetComponent<Brick>().posY = y;

        --funcCount;
    }

}
