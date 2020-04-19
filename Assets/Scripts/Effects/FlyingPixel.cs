using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlyingPixel : MonoBehaviour
{
    const float Speed = 10f;
    Vector3 start, finish;
    Color color;
    Action onFinish;
    float delay;

    void Start()
    {
        transform.position = start;
        GetComponent<SpriteRenderer>().color = color;
    }

    void Update()
    {
        if (delay > 0f)
        {
            delay -= Time.deltaTime;
            return;
        }
        transform.position += (finish - transform.position) * (Time.deltaTime * Speed);
        if ((transform.position - finish).magnitude < 0.1f)
        {
            onFinish?.Invoke();
            Destroy(gameObject);
        }
    }
    
    public static void Create(Vector2 start, Vector2 finish, Color color, Action a)
    {
        var p = Instantiate(FieldedObjects.Instance.Pixel).GetComponent<FlyingPixel>();
        p.start = start;
        p.finish = finish;
        p.color = color;
        p.onFinish = a;
        p.delay = Random.Range(0f, 0.4f);
    }
}