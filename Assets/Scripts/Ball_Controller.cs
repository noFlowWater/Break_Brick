using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ball_Controller : MonoBehaviour
{
    Rigidbody2D rigid;

    public float ball_speed;
    public Vector3 first_Dir;
    public Vector3 first_Pos;

    public float maxVelocityX, maxVelocityY;

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

            rigid.AddForce(first_Dir * 15 * ball_speed *Time.fixedDeltaTime);
            ball_first_move = false;
        }

        if (life_count <= 0)
        {
            life_count = 5;
            gameObject.SetActive(false);
        }
        //limitMoveSpeed();
}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            life_count--;
        }
    }

    void limitMoveSpeed()
    {
        if (rigid.velocity.x > maxVelocityX)
        {
            rigid.velocity = new Vector2(maxVelocityX, rigid.velocity.y);
        }
        if (rigid.velocity.x < (maxVelocityX * -1))
        {
            rigid.velocity = new Vector2((maxVelocityX * -1), rigid.velocity.y);
        }

        if (rigid.velocity.y > maxVelocityY)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, maxVelocityY);
        }
        if (rigid.velocity.y < (maxVelocityY * -1))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, (maxVelocityY * -1));
        }
    }
}
