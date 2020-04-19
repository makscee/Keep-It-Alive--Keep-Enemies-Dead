using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class MouseEventHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseDown();
        }
    }

    static void LeftMouseDown()
    {
        var pos = FieldedObjects.Instance.Camera.ScreenToWorldPoint(Input.mousePosition);

        if (!Shape.HeldShape)
        {
            foreach (var field in TetrisField.Fields)
            {
                field.HandleMouse(pos);
            }
        }

        var shape = Shape.HeldShape;
        if (shape && shape.allShadowsSet && shape.mouseAttached)
        {
            shape.allShadowsSet.PlaceShape(shape);
            shape.mouseAttached = false;
            Shape.HeldShape = null;
        } else if (shape && shape.mouseAttached)
        {
            foreach (var block in shape.Blocks)
            {
                if (block.Shadow.isActiveAndEnabled) return;
            }

            shape.falling = true;
            shape.mouseAttached = false;
            Shape.HeldShape = null;
        }
    }
}
