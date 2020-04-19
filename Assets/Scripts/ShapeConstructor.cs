using UnityEngine;

public static class ShapeConstructor
{
    public static Shape GetRandom()
    {
        var shape = new GameObject("Shape").AddComponent<Shape>();
        shape.transform.position = Vector3.zero;
        var t = ShapeTemplates.GetRandom();
        var s = t.shape;
        shape.Color = t.color;

        for (var _y = 0; _y < 4; _y++)
        {
            var y = 3 - _y;
            for (var x = 0; x < 4; x++)
            {
                if (s[_y * 4 + x] == ' ') continue;
                
                var block = Object.Instantiate(FieldedObjects.Instance.ShapeBlock, shape.transform).GetComponent<ShapeBlock>();
                var shadow = Object.Instantiate(FieldedObjects.Instance.ShapeShadowBlock, block.transform).GetComponent<ShapeShadowBlock>();
                shadow.gameObject.SetActive(false);
                block.Color = t.color;
                block.Shadow = shadow;
                block.X = x;
                block.Y = y;
                block.transform.position = new Vector3(x, y);
            }
        }
        return shape;
    }
}
