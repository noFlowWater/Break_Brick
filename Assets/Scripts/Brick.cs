using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Brick : MonoBehaviour
{
    public int life;

    public bool isBroken;

    public float posX;
    public float posY;

    public float shakeTime = 0.1f;
    public float shakeSpeed = 2.0f;
    public float shakeAmount = 1.0f;

    public bool isMove;

    Vector3 velo = Vector3.zero;
    Vector3 target;
    Color color = Color.white;
    protected virtual void Awake()
    {

        isBroken = false;
        // 6: red, 7: blue
        if (transform.gameObject.layer == 6) { color = new Color(255 / 255f, 180 / 255f, 180 / 255f); }
        else if (transform.gameObject.layer == 7) { color = new Color(180 / 255f, 225 / 255f, 255 / 255f); }
        life = (int)GameManager.instance.level;
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
            // 색깔 다를 때,
            if ((collision.gameObject.layer - this.gameObject.layer) != 2)
            {
                // Debug.Log("다름!");
                OnDamaged(2);
            }
            else
            {
                // Debug.Log("같음,,!");
                OnDamaged(1);
            }// 같을 때
        }
    }


    public virtual void OnDamaged(int damage)
    {
        life = life - damage;

        if (life <= 0 && !isBroken)
        {
            Break();
        }
        StartCoroutine(Shake());
    }

    protected virtual void Break()
    {
        isBroken = true;
        if (!GameManager.instance.isGameOver)
        {

            if (GameManager.instance.score > GameManager.instance.data.bestScore)
            {
                GameManager.instance.data.bestScore = GameManager.instance.score;
                GameManager.instance.bestScore = GameManager.instance.data.bestScore;
                // Debug.Log(GameManager.instance.data.bestScore);
                GameManager.instance.SaveUserData();
            }
        }
        GameManager.CreateParticleEffect(2, transform.position, transform.localRotation, color);
        Destroy(this.gameObject);
    }

    protected IEnumerator Shake()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < shakeTime)
        {
            Vector3 randomPoint = Vector3.zero + Random.insideUnitSphere * shakeAmount;
            transform.GetChild(0).localPosition = Vector3.Lerp(transform.GetChild(0).localPosition, randomPoint, Time.deltaTime * shakeSpeed);

            yield return null;

            elapsedTime += Time.deltaTime;
        }

        transform.GetChild(0).localPosition = Vector3.zero;
    }
}
