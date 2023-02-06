using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    // 블록들은 가로 1, 세로 0.5의 길이를 가짐
    public int life;

    private void Awake()
    {
        life = GameManager.instance.level;
    }

    private void Update()
    {
        
        float dist = Vector3.Distance(transform.position, GameManager.instance.mainCamera.transform.position);
        if(life<=0 || dist > 1000) { Destroy(gameObject); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            --life;
        }
    }

}
