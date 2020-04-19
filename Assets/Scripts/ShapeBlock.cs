using System;
using UnityEngine;

public class ShapeBlock : MonoBehaviour
{
    public int X, Y;
    public ShapeShadowBlock Shadow;

    public Color Color
    {
        set => _sr.color = value;
    }

    SpriteRenderer _sr;
    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }
}
