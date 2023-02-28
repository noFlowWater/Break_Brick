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
    SpriteRenderer spr;




    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();

        spr.sortingLayerName = sortingLayerName;
        spr.sortingOrder = sortingOrder;

        life_count = GameManager.instance.durability;

        rigid = GetComponent<Rigidbody2D>();


    }


    void OnEnable()
    {
        TrailRenderer trail = GetComponent<TrailRenderer>();
        ball_first_move = true;

        if (GameManager.instance.color == 0)
        {
            spr.color = new Color(180 / 255f, 225 / 255f, 255 / 255f);
        }
        else
        {
            spr.color = new Color(255 / 255f, 180 / 255f, 180 / 255f);
        }
        // trail.material.color = spr.color;

        trail.startColor = new Color(spr.color.r, spr.color.g, spr.color.b, 100 / 255f);
    }
    void Update()
    {

        if (ball_first_move)
        {
            ++GameManager.instance.ballNum;

            rigid.AddForce(first_Dir * 2000 * GameManager.instance.ballSpeed * Time.fixedDeltaTime);

            ball_first_move = false;


        }
        // rigid.AddForce(rigid.velocity * 100 * GameManager.instance.ballSpeed * Time.fixedDeltaTime);
        if (rigid.velocity.magnitude < GameManager.instance.ballSpeed)
        {
            // Debug.Log(rigid.velocity.magnitude);
            rigid.velocity = rigid.velocity.normalized * GameManager.instance.ballSpeed;
        }
        // else if (rigid.velocity != Vector2.zero)
        // {
        //     rigid.velocity = rigid.velocity.normalized * 2000 * GameManager.instance.ballSpeed * Time.fixedDeltaTime;
        // }

        if (life_count <= 0)
        {
            life_count = GameManager.instance.durability;
            --GameManager.instance.ballNum;
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
