using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ball_Gen_Controller : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    public Vector3 LoadedPos;
    Vector3 start_Pos, end_Pos, dirc;

    //private float lineWidth = 0.01f;
    private LineRenderer lr;
    private Vector3[] linePoints = new Vector3[2];

    float timer;
    float waitingTime;

    public bool onFire;
    public bool isMouseDownFirst;
    public bool isMouseDragFirst = false;
    public bool canMouseDown = true;

    int maxBallNum = 20;
    int ballNum;

    private void Start()
    {
        timer = 0.0f;
        waitingTime = 0.05f;
        ballNum = maxBallNum;

        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        lr.positionCount = linePoints.Length;
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

            if (ballNum == 0)
            {
                ballNum = maxBallNum;
                onFire = false;
                canMouseDown = true;
                
            }
        }
    }

    private void OnMouseDown()
    {
        if (!onFire && canMouseDown)
        {

            Debug.Log(" -- Mouse DOWN -- ");

            canMouseDown = false;
            isMouseDownFirst = true;
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

            start_Pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    private void OnMouseDrag()
    {
        //start_Pos 과 mousePos 를 이용하여 화살표를 표현하고싶...!
        if (!onFire && isMouseDownFirst)
        {
            if (!isMouseDragFirst)
            {
                isMouseDragFirst = true;
            }

            Debug.Log(" -- Mouse DRAG -- ");

            Vector3 myPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Vector3 dirc = (myPos - start_Pos).normalized; ;

            lr.enabled = true;
            linePoints[0] = start_Pos;
            linePoints[1] = myPos;
            lr.SetPositions(linePoints);

            transform.rotation = Quaternion.Euler(0, 0, 2 * GetAngle(new Vector3(1, 0, 0), dirc));
        }
    }

    private void OnMouseUp()//클릭을 땠을 때.
    {
        if (!onFire && isMouseDragFirst)
        {

            Debug.Log(" -- Mouse UP -- ");

            isMouseDragFirst = false;
            isMouseDownFirst = false;

            lr.enabled = false;
            spriteRenderer.color = new Color(1f, 1f, 1f, 0);

            end_Pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            dirc = (end_Pos - start_Pos).normalized;

            if (dirc != Vector3.zero)
            {
                Fire();
            }
            else
            {
                canMouseDown = true;
            }
        }
    }

    void Fire()
    {
        onFire = true;
        Debug.Log("Fire?");
    }

    void CreatBall(Vector3 dirc, Vector3 position)
    {
        GameObject ball = GameManager.instance.pool.Get(0);
        ball.transform.position = position;
        ball.GetComponent<Ball_Controller>().first_Dir = dirc;
        ballNum--;
    }

    public static float GetAngle(Vector3 vStart, Vector3 vEnd)
    {
        Vector3 v = vEnd - vStart;

        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

}