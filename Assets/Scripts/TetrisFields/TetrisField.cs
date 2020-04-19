using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TetrisField : MonoBehaviour
{
    public int H = 10, W = 5;
    protected Color Color = new Color(1f, 0.93f, 0.95f);

    bool[,] _fieldFlags;
    TetrisFieldBlock[,] _fieldBlocks;
    List<TetrisFieldBlock> _blocks;

    public static TetrisField PlayerField, EnemyField, InventoryField;
    public static readonly TetrisField[] Fields = new TetrisField[3];

    public Vector2 StartPos { get; protected set; }

    protected virtual void OnEnable()
    {
        _fieldFlags = new bool[W, H];
        _fieldBlocks = new TetrisFieldBlock[W, H];
        _blocks = new List<TetrisFieldBlock>(H * W);

        Init();

        for (var x = 0; x < W; x++)
        for (var y = 0; y < H; y++)
        { 
            var anchor = new Vector2(x, y);
            var block = CreateBlock(x, y);
            _fieldBlocks[x, y] = block;
        }
    }

    protected void Init()
    {
        transform.position = StartPos + new Vector2(W / 2f - 0.5f, H / 2f - 0.5f);
        transform.localScale = new Vector3(W, H, 1);

        var c = Color;
        c.a = 0.25f;
        GetComponent<SpriteRenderer>().color = c;
    }

    TetrisFieldBlock CreateBlock(int x, int y)
    {
        var block = TetrisFieldBlock.CreateBlock(StartPos, new Vector2(x, y));
        block.gameObject.SetActive(false);
        block.transform.SetParent(transform);
        block.GetComponent<SpriteRenderer>().color = Color;
        block.MoveToAnchor();
        _blocks.Add(block);
        return block;
    }

    TetrisFieldBlock GetOrCreateBlock(int x, int y)
    {
        var anchor = new Vector2(x, y);
        foreach (var block in _blocks)
        {
            if (!block.gameObject.activeSelf)
            {
                block.gameObject.SetActive(true);
                block.AnchorPos = anchor;
                block.MoveToAnchor();
                return block;
            }
        }

        return CreateBlock(x, y);
    }

    void CheckLines()
    {
        for (var y = H - 1; y >= 0; y--)
        for (var x = 0; x < W; x++)
        {
            if (!_fieldFlags[x, y]) break;
            if (x == W - 1)
            {
                CompleteLine(y);
            }
        }
    }

    public static int LinesCleared;
    protected virtual void CompleteLine(int yLine)
    {
        LinesCleared++;
        DisableLine(yLine);
        
        for (var y = yLine + 1; y < H; y++)
        for (var x = 0; x < W; x++)
        {
            if (!_fieldFlags[x, y]) continue;
            
            var block = _fieldBlocks[x, y];
            if (!block)
            {
                Debug.LogError($"Absent block! ({x}; {y})");
                continue;
            }

            MoveBlockDown(block);
        }
    }

    void MoveBlockDown(TetrisFieldBlock block)
    {
        MoveBlock(block, block.X, block.Y - 1);
    }

    void MoveBlock(TetrisFieldBlock block, int x, int y)
    {
        if (_fieldFlags[x, y])
        {
            Debug.LogWarning($"Position occupied ({x}; {y})");
        }
        int xOld = block.X, yOld = block.Y;

        _fieldFlags[xOld, yOld] = false;
        _fieldBlocks[xOld, yOld] = null;

        _fieldFlags[x, y] = true;
        _fieldBlocks[x, y] = block;
        block.AnchorPos = new Vector3(x, y);
    }

    void DisableLine(int y)
    {
        for (var x = 0; x < W; x++)
        {
            _fieldFlags[x, y] = false;
            _fieldBlocks[x, y].gameObject.SetActive(false);
        }
    }

    protected virtual void OnDisable()
    {
        for (var i = 0; i < _blocks.Count; i++)
        {
            if (_blocks[i] == null) continue;
            Destroy(_blocks[i].gameObject);
            _blocks[i] = null;
        }
    }

    public static int ShapesPlaced;
    public virtual void PlaceShape(Shape shape)
    {
        foreach (var block in shape.Blocks)
        {
            var shadow = block.Shadow;
            AddBlock(shadow.X, shadow.Y);
        }
        Destroy(shape.gameObject);
        ShapesPlaced++;

        CheckLines();
    }

    public virtual void AddBlock(int x, int y)
    {
        if (x < 0 || x >= W || y < 0 || y >= H)
        {
            Debug.LogError($"Bad coords provided ({x}; {y}) {gameObject}");
            return;
        }

        if (_fieldFlags[x, y])
        {
            Debug.Log("Trying to add existing block to field");
            return;
        }
        _fieldFlags[x, y] = true;
        var block = _fieldBlocks[x, y];
        if (block != null)
        {
            if (block.gameObject.activeSelf)
            {
                Debug.LogWarning($"Enabled object on false flag ({x}; {y}) {block.X} {block.Y}");
                if (block.X != x || block.Y != y)
                {
                    if (_fieldBlocks[block.X, block.Y] != block)
                    {
                        Debug.Log("disabling bad block");
                        block.gameObject.SetActive(false);
                    }
                    else
                    {
                        Debug.Log("dup block");
                        _fieldBlocks[x, y] = null;
                        block = GetOrCreateBlock(x, y);
                        _fieldBlocks[x, y] = block;
                        block.gameObject.SetActive(true);
                    }
                }
            }
            block.gameObject.SetActive(true);
        }
        else
        {
            block = GetOrCreateBlock(x, y);
            _fieldBlocks[x, y] = block;
            block.gameObject.SetActive(true);
        }
        block.AnchorPos = new Vector3(x, y);
        block.MoveToAnchor();
    }

    public void CleanUp()
    {
        foreach (var block in _blocks)
        {
            block.gameObject.SetActive(false);
        }
        for (var y = 0; y < H; y++)
        for (var x = 0; x < W; x++)
        {
            if (_fieldFlags[x, y]) GetOrCreateBlock(x, y);
        }
    }

    public virtual void HandleMouse(Vector2 pos)
    {
        // var coords = GetFieldBlockCoords(pos);
        // if (coords == null) return;
        //
        // AddBlock(coords[0], coords[1]);
    }

    public virtual bool IsOccupied(int x, int y)
    {
        return _fieldFlags[x, y];
    }

    public int[] GetFieldBlockCoords(Vector2 pos)
    {
        var bottomLeft = StartPos - new Vector2(0.5f, 0.5f);
        var topRight = bottomLeft + new Vector2(W, H);
        
        if (pos.x < bottomLeft.x || pos.y < bottomLeft.y || pos.x > topRight.x || pos.y > topRight.y) return null;
        pos -= bottomLeft;
        int x = (int) Math.Floor(pos.x), y = (int) Math.Floor(pos.y);
        return new[] {x, y};
    }
}
