using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBrick : Brick
{
    void Start()
    {
        life = 1;
    }
    protected override void Break()
    {
        base.Break();
    }
}
