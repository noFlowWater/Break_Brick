using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    Camera camera;

    void Awake()
    {
        camera = GetComponent<Camera>();

        Rect rt = camera.rect;

        float scale_height = ((float)Screen.width / Screen.height) / ((float)9 / 16);
        float scale_width = 1f / scale_height;

        if (scale_height < 1)
        {
            rt.height = scale_height;
            rt.y = (1f - scale_height) / 2f;
        }
        else
        {
            rt.width = scale_width;
            rt.x = (1f - scale_width) / 2f;
        }
        camera.rect = rt;
    }


}
