using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ball_Controller : MonoBehaviour
{
    Rigidbody2D rigid;

    public float ball_speed = 15;
    public Vector3 first_Dir;
    public Vector3 first_Pos;

    public int life_count = 5;
    bool ball_first_move;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        ball_first_move = true;
    }
    void Update()
    {
        if (ball_first_move)
        {

            rigid.AddForce(first_Dir * 2000 * ball_speed *Time.fixedDeltaTime);
            ball_first_move = false;
        }

        if (life_count <= 0)
        {
            life_count = 5;
            gameObject.SetActive(false);
        }
}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            life_count--;
        }
    }

    
}
