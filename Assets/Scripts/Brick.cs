using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Brick : MonoBehaviour
{

    // 블록들은 가로 1, 세로 0.5의 길이를 가짐
    public int life;
    public bool isBroken;

    public float posX;
    public float posY;

    Vector3 velo = Vector3.zero;
    Vector3 target;
    private void Awake()
    {
        life = GameManager.instance.level;
        isBroken = false;
    }

    void Update()
    {
        target = new Vector3(posX, posY, 0);

        transform.position = Vector3.SmoothDamp(transform.position, target, ref velo, GameManager.instance.ballSpeed * Time.smoothDeltaTime);

        // if (GameManager.instance.isPlayerTurn && GameManager.instance.funcCount == 0) { transform.name = "(" + transform.position.x + ", " + transform.position.y + ")"; }
    }

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

        // gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
