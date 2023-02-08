using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBrick : Brick
{
    protected override void Break()
    {
        ++GameManager.instance.life;
        base.Break();
    }
}
