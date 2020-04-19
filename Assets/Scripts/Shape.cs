using System;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public static Shape HeldShape;

    Vector2 _bottomLeft = Vector2.zero, _topRight, _mid;
    public Color Color;

    public ShapeBlock[] Blocks { get; set; }

    void OnEnable()
    {
        if (Blocks == null || Blocks.Length == 0)
        {
            InitBlocks();
        }
    }

    public bool mouseAttached;
    public bool falling;
    public TetrisField allShadowsSet;

    readonly Vector3 Gravity = new Vector3(0f, -14f, 0f);
    public Vector3 velocity;
    float _speed = 1f;
    void Update()
    {
        if (Blocks == null || Blocks.Length == 0)
        {
            InitBlocks();
        }

        if (falling)
        {
            transform.position += velocity * (Time.deltaTime * _speed);
            var x = velocity.x;
            velocity += (Gravity - velocity) * Time.deltaTime;
            velocity.x = x;
            if (Input.GetMouseButtonDown(0))
            {
                var mousePos =  FieldedObjects.Instance.Camera.ScreenToWorldPoint(Input.mousePosition);
                var v = new Vector2(0.5f, 0.5f);
                foreach (var block in Blocks)
                {
                    if (mousePos.x > block.transform.position.x - v.x &&
                        mousePos.y > block.transform.position.y - v.y &&
                        mousePos.x < block.transform.position.x + v.x &&
                        mousePos.y < block.transform.position.y + v.y)
                    {
                        falling = false;
                        mouseAttached = true;
                        return;
                    }
                }
            }
            if (transform.position.y < -50f) Destroy(gameObject);
        }
        else
        {
            velocity = Vector3.zero;
        }

        if (!mouseAttached) return;
        
        HeldShape = this;
        var pos = (Vector2)FieldedObjects.Instance.Camera.ScreenToWorldPoint(Input.mousePosition).SetZ(0f) - _mid;
        transform.position = pos;
        HideShadows();
        foreach (var field in TetrisField.Fields)
        {
            if (!field) continue;
            allShadowsSet = field;
            foreach (var block in Blocks)
            {
                if (block.Shadow.gameObject.activeSelf)
                {
                    allShadowsSet = null;
                    continue;
                }
                
                var coords = field.GetFieldBlockCoords(block.transform.position);
                if (coords != null && !field.IsOccupied(coords[0], coords[1])) block.Shadow.ShowOnField(field, coords[0], coords[1]);
                else allShadowsSet = null;
            }

            if (allShadowsSet) break;
        }

        foreach (var block in Blocks)
            block.Shadow.SetColor(allShadowsSet);
    }

    void HideShadows()
    {
        foreach (var block in Blocks)
            block.Shadow.Hide();
    }

    void InitBlocks()
    {
        Blocks = GetComponentsInChildren<ShapeBlock>();
        var mx = -1f;
        var my = -1f;
        foreach (var b in Blocks)
        {
            mx = Math.Max(mx, b.X);
            my = Math.Max(my, b.Y);
            b.Color = Color;
        }
        _topRight = new Vector2(mx, my);
        _mid = new Vector2((_bottomLeft.x + _topRight.x) / 2f, (_bottomLeft.y + _topRight.y) / 2f);
    }
}
