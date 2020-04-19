using System;
using UnityEngine;

public class ShapeShadowBlock : MonoBehaviour
{
    public int X, Y;
    SpriteRenderer _sr;

    void OnEnable()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    public void ShowOnField(TetrisField field, int x, int y)
    {
        transform.position = field.StartPos + new Vector2(x, y);
        gameObject.SetActive(true);
        X = x;
        Y = y;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetColor(bool positive)
    {
        _sr.color = positive ? new Color(0.44f, 1f, 0.57f, 0.5f) : new Color(1f, 0.59f, 0.59f, 0.5f);
    }
}