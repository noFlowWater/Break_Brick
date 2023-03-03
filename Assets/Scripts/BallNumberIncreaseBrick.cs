using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNumberIncreaseBrick : Brick
{
    void Start()
    {
        life = 1;
    }

    protected override void OnDamaged(int damage)
    {
        base.OnDamaged(damage);
    }
    protected override void Break()
    {
        GameManager.instance.level += 0.3f;
        GameManager.instance.ballNumber += 1;
        base.Break();
        // Debug.Log("Break");
    }

}
