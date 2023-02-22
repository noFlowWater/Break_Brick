using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNumberIncreaseBrick : Brick
{
    protected override void OnDamaged()
    {
        base.OnDamaged();
    }
    protected override void Break()
    {
        GameManager.instance.level += 1;
        GameManager.instance.ballNumber += 1;
        base.Break();
        Debug.Log("Break");
    }

}
