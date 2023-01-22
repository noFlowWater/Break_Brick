using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball_Gen_Controller : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    public Vector3 LoadedPos;
    Vector3 start_Pos, end_Pos, dirc;
    private Vector3 line_StartPos;


    public Image lineImg;

    float timer;
    float waitingTime;

    bool onFire = false;

    int ball_num = 5;

    private void Start()
    {
        timer = 0.0f;
        waitingTime = 0.1f;
    }

    private void Update()
    {
        Vector2 mousePos;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector2(mousePos.x, mousePos.y);

        if (onFire)
        {
            timer += Time.deltaTime;

            if (timer > waitingTime)
            {
                CreatBall(dirc, start_Pos);
                timer = 0;
            }

            if (ball_num == 0)
            {
                ball_num = 5;
                onFire = false;
            }
        }
    }



    private void OnMouseDown()
    {
        if (onFire)
            return;
        spriteRenderer.color = new Color(1f, 1f, 1f, .5f);
            
        start_Pos = new Vector3(transform.position.x, transform.position.y,transform.position.z);

        lineImg.gameObject.SetActive(true);
        line_StartPos = Input.mousePosition;
        lineImg.transform.position = line_StartPos;

    }

    private void OnMouseDrag()
    {
        //start_Pos 과 mousePos 를 이용하여 화살표를 표현하고싶...!
        if (onFire)
            return;
        Vector3 myPos = Input.mousePosition;
        lineImg.transform.localScale = new Vector2(Vector3.Distance(myPos, line_StartPos), 1);
        lineImg.transform.localRotation = Quaternion.Euler(0, 0,
            AngleInDeg(line_StartPos, myPos));
    }
    private void OnMouseUp()
    {//클릭을 땠을 때.
        if (onFire)
            return;
        lineImg.gameObject.SetActive(false);
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

        end_Pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        dirc = (end_Pos - start_Pos).normalized;

        if(dirc != Vector3.zero)
            Fire();
    }

    public static float AngleInRad(Vector3 vec1, Vector3 vec2)
    {
        return Mathf.Atan2(vec2.y - vec1.y, vec2.x - vec1.x);
    }

    public static float AngleInDeg(Vector3 vec1, Vector3 vec2)
    {
        return AngleInRad(vec1, vec2) * 180 / Mathf.PI;
    }

    void Fire()
    {
        onFire = true;
        
    }
    void CreatBall(Vector3 dirc, Vector3 position)
    {
        onFire = true;
        GameObject ball = GameManager.instance.pool.Get(0);
        ball.transform.position = position;
        ball.GetComponent<Ball_Controller>().ball_speed = 2000;
        ball.GetComponent<Ball_Controller>().first_Dir = dirc;
        ball_num--;
    }

}