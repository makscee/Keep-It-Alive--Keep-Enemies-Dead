using UnityEngine;
using UnityEngine.UI;

public class DmgText : MonoBehaviour
{
    public Text text;
    readonly Vector3 Gravity = new Vector3(0f, -15f, 0f);
    Vector3 _velocity;
    float _speed = 2f;

    void Update()
    {
        transform.position += _velocity * (Time.deltaTime * _speed);
        var x = _velocity.x;
        _velocity += (Gravity - _velocity) * Time.deltaTime;
        _velocity.x = x;
        if (transform.position.y < -10f) Destroy(gameObject);
    }

    public static void Create(int value, Vector3 pos)
    {
        var d = Instantiate(FieldedObjects.Instance.DmgText, FieldedObjects.Instance.Canvas.transform).GetComponent<DmgText>();
        d.transform.position = pos;
        d.text.text = value.ToString();
        d._velocity = new Vector3(Random.Range(-5f, 2f), 5f);
    }
}