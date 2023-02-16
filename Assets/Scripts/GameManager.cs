using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public MainCamera mainCamera;
    public PoolManager poolManager;

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


    private void Update()
    {
        GameObject ball = null;
        ball = GameObject.FindWithTag("Ball");
        isPlayerTurn = ball == null ? true : false;
    }

}
