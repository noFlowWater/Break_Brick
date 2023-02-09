using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{


    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (!collision.CompareTag("Area"))
    //        return;

    //    Vector3 cameraPos = GameManager.instance.MainCamera.transform.position;
    //    Vector3 myPos = transform.position;

    //    //Debug.Log(cameraPos);
    //    //Debug.Log(myPos);

    //    float diffX = Mathf.Abs(cameraPos.x - myPos.x);
    //    float diffY = Mathf.Abs(cameraPos.y - myPos.y);

    //    float playerDirX = GameManager.instance.MainCamera.transform.position.x - GameManager.instance.transform.position.x;
    //    float playerDirY = GameManager.instance.MainCamera.transform.position.y - GameManager.instance.transform.position.y;
    //    float dirX = playerDirX < 0 ? -1 : 1;
    //    float dirY = playerDirY < 0 ? -1 : 1;

    //    Debug.Log("X:"+playerDirX);
    //    Debug.Log("Y:"+playerDirY);

    //    switch (transform.tag)
    //    {
    //        case "Background":
    //            if (diffX > diffY)
    //            {
    //                transform.Translate(Vector3.right * dirX * 80);
    //            }
    //            else if (diffX < diffY)
    //            {
    //                transform.Translate(Vector3.up * dirY * 56);
    //            }
    //            break;
    //        default:
    //            break;
    //    }

    //}

    private void Update()
    {
        Vector3 cameraPos = GameManager.instance.mainCamera.transform.position;
        Vector3 myPos = transform.position;

        float diffX = cameraPos.x - myPos.x;
        float diffY = cameraPos.y - myPos.y;

        if (diffX < -40) { transform.Translate(Vector2.left * 80); }
        else if (diffX > 40) { transform.Translate(Vector2.right * 80); }
        if (diffY < -28) { transform.Translate(Vector3.down * 56); }
        else if (diffY > 28) { transform.Translate(Vector3.up * 56); }
    }

}
