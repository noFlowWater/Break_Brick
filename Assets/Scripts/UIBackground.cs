using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBackground : MonoBehaviour
{
    SpriteRenderer spr;
    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (GameManager.instance.color == 0) //Blue
        {
            spr.color = new Color(180 / 255f, 225 / 255f, 255 / 255f);
        }
        else
        {
            spr.color = new Color(255 / 255f, 180 / 255f, 180 / 255f);
        }
    }
}
