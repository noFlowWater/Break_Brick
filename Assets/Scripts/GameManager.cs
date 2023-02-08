using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public MainCamera mainCamera;
    public PoolManager poolManager;

    public float level;
    public int ballNumber;
    public int life;
    public int durability;
    public int score;

    private void Awake()
    {
        instance = this;
        level = 1;
        ballNumber = 1;
        life = 1;
        durability = 1;
        score = 0;
    }

    
}