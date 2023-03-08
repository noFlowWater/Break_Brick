using TMPro;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PoolManager poolManager;
    // public GameObject[] VerticalChecker;
    // public GameObject[] HorizontalChecker;
    public Spawner spawner;
    public Ball_Gen_Controller bgc;
    public UserData data;


    public float level;
    float levelAdd;

    public int score;
    public TextMeshProUGUI scoreTxt;
    public int bestScore;
    public TextMeshProUGUI bestScoreTxt;
    public int ballNumber;
    public TextMeshProUGUI ballNumberTxt;
    public int life;
    public TextMeshProUGUI lifeTxt;
    public int durability;
    public TextMeshProUGUI durabilityTxt;

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
    // public bool startGame;

    public bool inTitle;
    public bool loading;

    private void Awake()
    {
        instance = this;
        score = 0;
        Time.timeScale = timeScale;
        // LineBreakCheck();
        color = 0;
        // if (inTitle)
        // {
        //     startGame = false;
        // }
        // else
        // {
        //     startGame = true;
        // }
    }

    void Start()
    {
        LoadUserData();
        Debug.Log("우왕 ㅋㅋ : " + data.bestScore);

        if (!inTitle)
        {
            DataManager.Instance.LoadGameData();
            spawner.InitSpawn();
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

    void LateUpdate()
    {
        if (!inTitle)
        {
            scoreTxt.text = string.Format("{0:n0}", score);
            lifeTxt.text = string.Format("{0:n0}", life);
            ballNumberTxt.text = string.Format("{0:n0}", ballNumber);
            durabilityTxt.text = string.Format("{0:n0}", durability);
        }
    }

    void OnApplicationQuit()
    {
        DataManager.Instance.SaveGameData();
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
        color = (color + 1) % 2;

        level += score / 10;
        durability = (int)(ballNumber / 2);
        DataManager.Instance.SaveGameData();
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
            if (level != 1)
            {
                ++ballNumber;

                level += 1f;
            }
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
            if (level != 1)
            {
                ++ballNumber;
                level += 1f;
            }
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
    //////////////////////////////////////////////////////////////////////////////////

    public void SaveUserData()
    {
        string filePath = Application.persistentDataPath + "/userdata.json";
        string jsonData = JsonConvert.SerializeObject(data);
        File.WriteAllText(filePath, jsonData);
    }
    void LoadUserData()
    {
        string filePath = Application.persistentDataPath + "/userdata.json";
        if (File.Exists(filePath))
        {
            try
            {
                string jsonData = File.ReadAllText(filePath);
                data = JsonConvert.DeserializeObject<UserData>(jsonData);
                Debug.Log(data.bestScore);
            }
            catch (JsonException ex)
            {
                Debug.LogError("Failed to deserialize. Reason: " + ex.Message);
                // 파일을 삭제하거나, 새로 생성하는 등의 예외 처리를 수행
            }
        }
        else
        {
            Debug.Log("No userdata found.");
            data = new UserData();
        }
    }
}

[Serializable]
public class UserData
{
    public int bestScore;
}
