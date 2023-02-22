using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Brick : MonoBehaviour
{

    // 블록들은 가로 1, 세로 0.5의 길이를 가짐
    public int life;
    public bool isBroken;


    private void Awake()
    {
        life = GameManager.instance.level;
        isBroken = false;
    }

    //private void Update()
    //{

    //    float dist = Vector3.Distance(transform.position, GameManager.instance.mainCamera.transform.position);
    //    Debug.Log(dist);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameObject effect = GameManager.instance.poolManager.Get(5);
            effect.transform.position = transform.position;
            OnDamaged();
            
        }
    }


    protected virtual void OnDamaged()
    {
        --life;
        if (life <= 0 && !isBroken)
        {
            Break();
        }
    }

    protected virtual void Break()
    {
        isBroken = true;
        ++GameManager.instance.score;

        gameObject.SetActive(false);
    }
}
