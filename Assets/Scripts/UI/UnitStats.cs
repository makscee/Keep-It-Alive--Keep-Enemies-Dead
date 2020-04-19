using UnityEngine;
using UnityEngine.UI;

public class UnitStats : MonoBehaviour
{
    public Unit anchor;
    public Vector3 offset;
    const float Speed = 5f;
    public int dmgBoosted;

    public Text damage, hp;
    public Outline outline;

    void Update()
    {
        if (!anchor)
        {
            Destroy(gameObject);
            return;
        }
        transform.position += (anchor.transform.position + offset - transform.position) * (Time.deltaTime * Speed);
    }

    public static UnitStats Create(Unit anchor, int hp, int dmgFrom, int dmgTo)
    {
        var h = Instantiate(FieldedObjects.Instance.HpText, FieldedObjects.Instance.Canvas.transform).GetComponent<UnitStats>();
        h.anchor = anchor;
        h.damage.text = $"{dmgFrom} - {dmgTo}";
        h.hp.text = hp.ToString();
        h.transform.position = anchor.transform.position + h.offset;
        return h;
    }

    public void Refresh()
    {
        damage.text = $"{anchor.damageFrom + dmgBoosted} - {anchor.damageTo + dmgBoosted}";
        hp.text = anchor.hp.ToString();
        if (dmgBoosted > 0)
        {
            damage.color = Color.blue;
        }
        else
        {
            damage.color = Color.white;;
        }
    }
}