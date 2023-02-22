using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public MainCamera mainCamera;
    public PoolManager poolManager;

    public int level;
    public int score;
    public int ballNumber;
    public int life;
    public int durability;
    public float fireTime;
    public float ballSpeed;

    private void Awake()
    {
        instance = this;
        level = 100;
        score = 0;
    }


}
