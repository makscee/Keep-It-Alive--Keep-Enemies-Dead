using System;
using UnityEngine;
public class InventoryField : TetrisField
{
    Shape[,] _shapes;
    protected override void OnEnable()
    {
        _shapes = new Shape[W, H];
        StartPos =  new Vector2(-2f, -H);
        Color = new Color(0.4f, 1f, 0.55f);
        InventoryField = this;
        Fields[2] = InventoryField;
        Init();
    }

    public override void PlaceShape(Shape shape)
    {
        var shapePos = new Vector2(999f, 999f);
        foreach (var block in shape.Blocks)
        {
            var shadow = block.Shadow;
            shadow.Hide();
            var x = shadow.X;
            var y = shadow.Y;
            
            _shapes[x, y] = shape;
            shapePos.x = Math.Min(shapePos.x, x);
            shapePos.y = Math.Min(shapePos.y, y);
        }
        shape.transform.position = shapePos + StartPos;
        ShapesPlaced++;
    }

    public override bool IsOccupied(int x, int y)
    {
        return _shapes[x, y];
    }

    public override void HandleMouse(Vector2 pos)
    {
        if (Shape.HeldShape) return;
        var coords = GetFieldBlockCoords(pos);
        if (coords == null) return;
        int x = coords[0], y = coords[1];
        var shape = _shapes[x, y];
        if (!shape) return;
        
        for (var xi = 0; xi < W; xi++)
        for (var yi = 0; yi < H; yi++)
        {
            if (_shapes[xi, yi] == shape) _shapes[xi, yi] = null;
        }

        shape.mouseAttached = true;
    }

    protected override void OnDisable()
    {
        foreach (var shape in _shapes)
        {
            if (!shape) continue;
            Destroy(shape.gameObject);
        }
    }

    public override void AddBlock(int x, int y) { }
}