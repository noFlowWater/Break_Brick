using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMeshInBrick : MonoBehaviour
{
    public string sortingLayerName;
    public int sortingOrder;
    public Brick brickObj;

    TextMeshPro tmp;

    void Awake()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.sortingLayerName = sortingLayerName;
        mesh.sortingOrder = sortingOrder;

        tmp = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        tmp.text = brickObj.life.ToString();
    }
}
