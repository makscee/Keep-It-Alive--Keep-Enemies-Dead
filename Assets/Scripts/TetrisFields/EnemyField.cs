using UnityEngine;
public class EnemyField : TetrisField
{   
    protected override void OnEnable()
    {
        Color = new Color(1f, 0.26f, 0.51f);
        StartPos = new Vector2(7f, 1f);
        EnemyField = this;
        Fields[1] = EnemyField;
        base.OnEnable();
    }
    
    protected override void CompleteLine(int yLine)
    {
        base.CompleteLine(yLine);
        for (var x = 0; x < W; x++)
        {
            var pos = new Vector2(x, yLine) + StartPos;
            FlyingPixel.Create(pos, Player.Instance.transform.position, new Color(0.74f, 0.02f, 0f, 0.76f), () => Player.Instance.AddDmg(1));
        }
    }
}