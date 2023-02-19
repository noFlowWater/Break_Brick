using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ball_Controller : MonoBehaviour
{

    public string sortingLayerName;
    public int sortingOrder;

    Rigidbody2D rigid;

    public Vector3 first_Dir;
    public Vector3 first_Pos;

    public int life_count;
    bool ball_first_move;

    void Awake()
    {
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        spr.sortingLayerName = sortingLayerName;
        spr.sortingOrder = sortingOrder;
        life_count = GameManager.instance.durability;

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

            rigid.AddForce(first_Dir * 2000 * GameManager.instance.ballSpeed * Time.fixedDeltaTime);

            ball_first_move = false;


        }
        // rigid.AddForce(rigid.velocity * 100 * GameManager.instance.ballSpeed * Time.fixedDeltaTime);
        rigid.velocity = rigid.velocity.normalized * GameManager.instance.ballSpeed;
        Debug.Log(rigid.velocity.magnitude);
        // else if (rigid.velocity != Vector2.zero)
        // {
        //     rigid.velocity = rigid.velocity.normalized * 2000 * GameManager.instance.ballSpeed * Time.fixedDeltaTime;
        // }

        if (life_count <= 0)
        {
            life_count = GameManager.instance.durability;
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
