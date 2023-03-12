using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNumberIncreaseBrick : Brick
{
    void Start()
    {
        life = 1;
    }

    public override void OnDamaged(int damage)
    {
        base.OnDamaged(damage); // 부모 클래스의 OnDamaged 메소드를 호출
        // 추가로 수행할 코드 작성
    }
    protected override void Break()
    {
        GameManager.ballNumberIncreaseBrickAudioSource.Play();
        GameManager.instance.ballNumber += 1;
        base.Break();
        // Debug.Log("Break");
    }

}
