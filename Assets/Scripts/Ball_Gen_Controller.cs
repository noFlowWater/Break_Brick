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
    // private LineRenderer lr;
    private Vector3[] linePoints = new Vector3[2];

    float timer;
    public float waitingTime;

    public bool onFire;
    public bool isMouseDownFirst;
    public bool isMouseDragFirst = false;
    bool inFireArea;

    // int maxBallNum = 20;
    public int ballNum;


    void Awake()
    {

    }

    private void Start()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 0);

        timer = 0.0f;
        // waitingTime = 0.025f;
        ballNum = GameManager.instance.ballNumber;

        // lr = GetComponent<LineRenderer>();
        // lr.enabled = false;
        // lr.positionCount = linePoints.Length;

        inFireArea = false;
    }

    private void FixedUpdate()
    {
        Vector2 mousePos;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector2(mousePos.x, mousePos.y);

        if (!onFire)
        {
            ballNum = GameManager.instance.ballNumber;
            GameObject ball = null;
            ball = GameObject.Find("Ball");
            if (ball == null && !onFire && GameManager.instance.isPlayerTurn != true)
            {
                GameManager.instance.LineBreakCheck();
                GameManager.instance.isPlayerTurn = true;
            }
        }

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
                ballNum = GameManager.instance.ballNumber;
                onFire = false;
            }
        }
    }

    private void OnMouseDown()
    {
        if (Mathf.Abs(transform.position.x) < Mathf.Abs(GameManager.instance.playerPlayPointX + 0.5f) &&
            Mathf.Abs(transform.position.y) < Mathf.Abs(GameManager.instance.playerPlayPointY + 0.5f))
        {
            inFireArea = true;
        }
        else
        {
            inFireArea = false;
        }
        if (!onFire && GameManager.instance.isPlayerTurn && !isMouseDownFirst && inFireArea)
        {

            // Debug.Log(" -- Mouse DOWN -- ");
            isMouseDownFirst = true;
            // spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            if (GameManager.instance.color == 1)
            {
                spriteRenderer.color = new Color(180 / 255f, 225 / 255f, 255 / 255f);
            }
            else
            {
                spriteRenderer.color = new Color(255 / 255f, 180 / 255f, 180 / 255f);
            }

            start_Pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    private void OnMouseDrag()
    {
        //start_Pos 과 mousePos 를 이용하여 화살표를 표현하고싶...!
        if (!onFire && GameManager.instance.isPlayerTurn)
        {
            if (!isMouseDragFirst)
            {
                isMouseDragFirst = true;
            }

            // Debug.Log(" -- Mouse DRAG -- ");

            Vector3 myPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Vector3 dirc = (myPos - start_Pos).normalized; ;

            linePoints[0] = start_Pos;
            linePoints[1] = myPos;
            // lr.enabled = true;
            // lr.SetPositions(linePoints);
            DottedLine.Instance.DrawDottedLine(start_Pos, myPos);

            transform.rotation = Quaternion.Euler(0, 0, 2 * GetAngle(new Vector3(1, 0, 0), dirc));
        }
    }

    private void OnMouseUp()//클릭을 땠을 때.
    {
        if (!onFire && isMouseDragFirst)
        {

            // Debug.Log(" -- Mouse UP -- ");

            isMouseDragFirst = false;
            isMouseDownFirst = false;

            // lr.enabled = false;
            spriteRenderer.color = new Color(1f, 1f, 1f, 0);

            end_Pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            dirc = (end_Pos - start_Pos).normalized;

            if (dirc != Vector3.zero && GameManager.instance.funcCount == 0)
            {
                Fire();
            }
        }

    }

    void Fire()
    {
        onFire = true;
        GameManager.instance.isPlayerTurn = false;
        GameManager.instance.startGame = false;
        --GameManager.instance.life;
    }

    void CreatBall(Vector3 dirc, Vector3 position)
    {
        GameObject ball = GameManager.instance.poolManager.Get(0);
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