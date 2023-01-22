using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Pool_Manager pool;

    void Awake()
    {
        instance = this;
    }
}
