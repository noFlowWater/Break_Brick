using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurabilityBrick : Brick
{
    protected override void Break()
    {
        ++GameManager.instance.durability;
        base.Break();
    }
}
