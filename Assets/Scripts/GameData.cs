using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class GameData
{
    public float level;
    public int score;

    public int ballNumber;
    public int life;
    public int durability;
    public int color;

    // public ArrayList bricks;
    public BrickData[] bricks;


}

[Serializable]

public class BrickData
{
    // 0: blue, 1: red
    public int color;
    // 1: normal, 2: life, 3: BallNumber
    public int type;

    // Brick클래스
    public int life;


    public float posX;
    public float posY;

    public bool isMove;

    // NormalBrick 클래스
    public int maxLife;
}