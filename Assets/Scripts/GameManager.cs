using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public MainCamera mainCamera;
    public PoolManager poolManager;

    public int level;

    private void Awake()
    {
        instance = this;
        level = 1;
    }

    
}