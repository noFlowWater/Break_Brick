using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mold : MonoBehaviour
{
    public bool needReturn;
    Color originColor;
    public SpriteRenderer spr;

    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        originColor = spr.color;
        Debug.Log(originColor);
        needReturn = true;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject brick = GameObject.Find("(" + transform.position.x + ", " + transform.position.y + ")");
    }

    public IEnumerator LineEffect(float delay)
    {
        needReturn = false;
        yield return new WaitForSeconds(.1f * delay * 10000);

        spr.color = Color.white;
    }


}
