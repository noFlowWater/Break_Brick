using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Vector3 touchPos;
    public Spawner spawner;

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
            // 한번 턴이 지날때마다 조금씩 level이 증가
            GameManager.instance.level += 0.1f;
            RepositionSpawner(transform.position.x, transform.position.y);
        }
    }

    void RepositionSpawner(float x, float y)
    {
        float newX = x - (x % 1);
        float newY = y - (y % 0.5f);
        //newX = newY % 1 == 0 ? newX : newX + 0.5f;

        spawner.transform.position = new Vector3(newX, newY, 0);
        spawner.Spawn();
    }
}
