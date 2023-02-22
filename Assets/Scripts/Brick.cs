using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    // 블록들은 가로 1, 세로 0.5의 길이를 가짐
    public int life;
    public bool isBroken;

    public float posX;
    public float posY;

    public bool isMove;

    Vector3 velo = Vector3.zero;
    Vector3 target;
    private void Awake()
    {
        life = (int)GameManager.instance.level;
        isBroken = false;
    }

    void Update()
    {
        target = new Vector3(posX, posY, 0);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velo, GameManager.instance.brickSpeed);

        // Debug.Log(transform.name + "/" + "(" + transform.position.x + ", " + transform.position.y + ")");
        // if (transform.name == "(" + transform.position.x + ", " + transform.position.y + ")" && isMove)
        // {
        //     --GameManager.instance.funcCount;
        //     isMove = false;
        // }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
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
        Destroy(this.gameObject);
    }
}
