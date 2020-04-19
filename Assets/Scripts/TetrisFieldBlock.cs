using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class TetrisFieldBlock : MonoBehaviour
{
    public Vector3 _anchorPos, StartPos;
    float MoveSpeed = 7f;

    Vector3 DesiredPosition => _anchorPos + StartPos;

    public int _x, _y;

    public Vector3 AnchorPos
    {
        get => _anchorPos;
        set
        {
            _anchorPos = value;
            _x = (int)value.x;
            _y = (int)value.y;
        }
    }

    public int X => _x;
    public int Y => _y;

    public void Awake()
    {
        MoveSpeed += Random.Range(-2f, 2f);
    }

    static GameObject Prefab;
    public static TetrisFieldBlock CreateBlock(Vector2 start, Vector2 anchor)
    {
        if (Prefab == null) Prefab = Resources.Load<GameObject>("TetrisFieldBlock");
        
        var go = Instantiate(Prefab);
        // go.transform.position = start + anchor;
        var t = go.GetComponent<TetrisFieldBlock>();

        t.AnchorPos = anchor;
        t.MoveToAnchor();
        t.StartPos = start;
        return t;
    }

    public void MoveToAnchor()
    {
        transform.position = DesiredPosition;
    }

    void Update()
    {
        if (!_smoothMoveInProgress && DesiredPosition != transform.position)
        {
            _smoothMoveInProgress = true;
            StartCoroutine("SmoothMove");
        }
    }

    bool _smoothMoveInProgress;
    IEnumerator SmoothMove()
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.2f));
        while (true)
        {
            if ((DesiredPosition - transform.position).magnitude < 0.01f)
            {
                transform.position = DesiredPosition;
                _smoothMoveInProgress = false;
                yield break;
            }

            transform.position += (DesiredPosition - transform.position) * (Time.deltaTime * MoveSpeed);
            yield return null;
        }
    }
}