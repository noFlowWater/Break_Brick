using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DottedLine : MonoBehaviour
{
    // Inspector fields
    public Sprite Dot;
    [Range(0.01f, 1f)]
    public float Size;
    [Range(0.1f, 2f)]
    public float Delta;
    [Range(0, 255)]
    // public float r;
    // [Range(0, 255)]
    // public float g;
    // [Range(0, 255)]
    // public float b;
    // [Range(0, 255)]
    // public float alpha;
    public string sortingLayerName;
    public int sortingOrder;

    //Static Property with backing field
    private static DottedLine instance;
    public static DottedLine Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<DottedLine>();
            return instance;
        }
    }

    //Utility fields
    List<Vector2> positions = new List<Vector2>();
    List<GameObject> dots = new List<GameObject>();

    // Update is called once per frame
    void FixedUpdate()
    {
        if (positions.Count > 0)
        {
            DestroyAllDots();
            positions.Clear();
        }

    }

    private void DestroyAllDots()
    {
        foreach (var dot in dots)
        {
            Destroy(dot);
        }
        dots.Clear();
    }

    GameObject GetOneDot()
    {
        var gameObject = new GameObject();
        gameObject.transform.localScale = Vector3.one * Size;
        gameObject.transform.parent = transform;

        var sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = Dot;
        // sr.color = new Color(r / 255, g / 255, b / 255, alpha);
        if (GameManager.instance.color == 0)
        {
            sr.color = new Color(180 / 255f, 225 / 255f, 255 / 255f);
        }
        else
        {
            sr.color = new Color(255 / 255f, 180 / 255f, 180 / 255f);
        }
        sr.sortingLayerName = sortingLayerName;
        sr.sortingOrder = sortingOrder;
        return gameObject;
    }

    public void DrawDottedLine(Vector2 start, Vector2 end)
    {
        DestroyAllDots();

        Vector2 point = start;
        Vector2 direction = (end - start).normalized;

        while ((end - start).magnitude > (point - start).magnitude)
        {
            positions.Add(point);
            point += (direction * Delta);
        }

        Render();
    }

    private void Render()
    {
        foreach (var position in positions)
        {
            var g = GetOneDot();
            g.transform.position = position;
            dots.Add(g);
        }
    }
}
