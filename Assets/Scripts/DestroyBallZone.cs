using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBallZone : MonoBehaviour
{
    public float rotateSpeed;
    public float buyuSpeed;
    public float buyuHeight;
    SpriteRenderer spr;

    int dir;
    // Start is called before the first frame update
    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        dir = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(new Vector3(0, 0, rotateSpeed) * Time.deltaTime);



        // if (transform.position.y < -buyuHeight)
        // {
        //     dir = 1;
        // }
        // else if (transform.position.y > buyuHeight)
        // {
        //     dir = -1;
        // }
        // transform.position = new Vector3(0, (dir * buyuSpeed * Time.deltaTime) + transform.position.y, 0);

        float alpha = 1 - (float)((float)GameManager.instance.ballNum / (float)GameManager.instance.ballNumber);

        if (GameManager.instance.color == 0) //Blue
        {
            spr.color = GameManager.instance.blueColor;
        }
        else
        {
            spr.color = GameManager.instance.redColor;
        }

        spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, alpha);
    }
}
