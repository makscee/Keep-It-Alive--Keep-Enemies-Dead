using UnityEngine;

public class PlayerField : TetrisField
{
    protected override void OnEnable()
    {
        StartPos = new Vector2(-2f, 1f);
        Color = new Color(0.4f, 0.51f, 1f);
        PlayerField = this;
        Fields[0] = PlayerField;
        base.OnEnable();
    }

    protected override void CompleteLine(int yLine)
    {
        base.CompleteLine(yLine);
        for (var x = 0; x < W; x++)
        {
            var pos = new Vector2(x, yLine) + StartPos;
            FlyingPixel.Create(pos, Player.Instance.transform.position, new Color(0f, 0.74f, 0.19f, 0.76f), () => Player.Instance.Heal());
        }
    }
}