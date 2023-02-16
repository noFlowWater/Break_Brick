using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public MainCamera mainCamera;
    public PoolManager poolManager;
    public GameObject[] VerticalChecker;
    public GameObject[] HorizontalChecker;

    public float level;
    public int score;
    public int ballNumber;
    public int life;
    public int durability;

    public float ballSpeed;

    public bool isPlayerTurn;

    private void Awake()
    {
        instance = this;
        score = 0;
    }


    void LineBreakCheck()
    {

    }

}
