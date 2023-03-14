using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotBreakableBrick : Brick
{

    protected override void Awake()
    {
        // base.Awake();
        life = -999;
    }
    void Update()
    {

    }
    public override void OnDamaged(int damage)
    {
        StartCoroutine(Shake());
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ball"))
        {
            TextMeshPro tmp = transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshInBrick>().tmp;
            print("!");
            tmp.text = "!";
            StartCoroutine(SetTextBlank(1, tmp));
        }
    }

    IEnumerator SetTextBlank(float delay, TextMeshPro tmp)
    {
        yield return new WaitForSeconds(delay);
        tmp.text = "";
    }


}