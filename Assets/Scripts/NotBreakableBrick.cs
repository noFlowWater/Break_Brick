using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotBreakableBrick : Brick
{
    protected override void Awake()
    {
        // base.Awake();
        life = -999;
    }
    void Update()
    {

    }
    public override void OnDamaged(int damage)
    {
        StartCoroutine(Shake());
    }

}