using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Vector3 touchPos;
    

    // Update is called once per frame
    void Update()
    {
        //Touch touch = Input.GetTouch(0);
        //if(touch.phase == TouchPhase.Began) { touchPos = touch.position; }
        //else if(touch.phase == TouchPhase.Ended) { transform.position = touchPos; }

            

        if (Input.GetMouseButtonDown(0)) { touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); }
        else if(Input.GetMouseButtonUp(0))
        {
            transform.position = touchPos;
        }

    }
}
