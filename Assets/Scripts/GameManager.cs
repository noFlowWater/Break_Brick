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
    public Ball_Gen_Controller bgc;


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

    public int ballNum;

    public int color;
    public bool startGame;

    public bool inTitle;

    private void Awake()
    {
        instance = this;
        score = 0;
        Time.timeScale = timeScale;
        // LineBreakCheck();
        color = 0;
        if (inTitle)
        {
            startGame = false;
        }
        else
        {
            startGame = true;
        }
    }

    void Update()
    {
        if (inTitle)
        {
            Time.timeScale = timeScale;
        }
        else
        {
            if (ballNum == 0)
            {
                Time.timeScale = timeScale;
            }
            // else if (ballNum < ballNumber * 0.2 && ballNumber > 10 && !bgc.onFire) { Time.timeScale = timeScale * 2; }
            else if (!bgc.onFire)
            {
                // Time.timeScale = (timeScale * 2) + (timeScale * 3 * (1 - (ballNum / ballNumber)));
                Time.timeScale = timeScale * (3f - 2f * ((float)(ballNum) / (float)(ballNumber)));
                // Time.timeScale = timeScale * (1 + Mathf.Log(ballNum / ballNumber, ballNumber));
                // Time.timeScale = timeScale * Mathf.Pow(2.5f, 1 - ((float)ballNum / (float)ballNumber));

            }
            // Debug.Log(Time.timeScale);}
        }
    }

    public void LineBreakCheck()
    {
        // Debug.Log("sibal");
        delay = -delayRate;
        HorizontalLineBreakCheck(1);
        delay = -delayRate;
        HorizontalLineBreakCheck(-1);
        delay = -delayRate;
        VerticalLineBreakCheck(1);
        delay = -delayRate;
        VerticalLineBreakCheck(-1);
        color = (color + 1) % 2;
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
                if (brick != null) { needFill = false; continue; }
                brick = GameObject.Find("(" + x + ", " + y + ")Mold");
                // if (brick.GetComponent<Mold>().spr.color != Color.white && !startGame)
                // { StartCoroutine(brick.GetComponent<Mold>().LineEffect(delay)); }
            }
            if (needFill)
            {
                // Debug.Log("테트리스!H");
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

        // Debug.Log("startXPos: " + startXPos);
        // Debug.Log("endXPos: " + endXPos);
        // Debug.Log("endYPos: " + endYPos);
        // Debug.Log("needY: " + needY);


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
            if (!startGame)
            { ++durability; }
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
                if (brick != null) { needFill = false; }
                brick = GameObject.Find("(" + x + ", " + y + ")Mold");
                // if (brick.GetComponent<Mold>().spr.color != Color.white && !startGame)
                // { StartCoroutine(brick.GetComponent<Mold>().LineEffect(delay)); }
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
            if (!startGame)
            { ++durability; }
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

        // GameObject mold = GameObject.Find("(" + x + ", " + y + ")Mold");
        // mold.GetComponent<Mold>().needReturn = true;

        --funcCount;
    }

    ///////////////////////////////////////////////////////////////////////////////////////


    public static void CreateParticleEffect(int index, Vector3 position, Quaternion rotation, Color tempColor)
    {
        GameObject particle = GameManager.instance.poolManager.Get(index);
        particle.transform.position = position;
        particle.transform.localRotation = rotation;
        PlayParticle(particle, tempColor);
    }

    static void PlayParticle(GameObject particle, Color tempColor)
    {
        ParticleSystem ps = particle.GetComponent<ParticleSystem>();
        //ps.GetComponent<Renderer>().material.color = tempColor;
        float duration = ps.main.duration + ps.main.startLifetime.constant;
        ps.GetComponent<Renderer>().material.color = tempColor;
        GameManager.instance.StartCoroutine(DeactivateAfterDuration(particle, duration));
    }

    private static IEnumerator DeactivateAfterDuration(GameObject particle, float duration)
    {
        yield return new WaitForSeconds(duration);
        particle.SetActive(false);
    }
}
