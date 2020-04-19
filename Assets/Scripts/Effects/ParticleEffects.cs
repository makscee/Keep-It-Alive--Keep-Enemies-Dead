using UnityEngine;

public class ParticleEffects
{
    public static void CreateExplosion(Vector2 pos)
    {
        var obj = Object.Instantiate(FieldedObjects.Instance.ParticleExplosion);
        Object.Destroy(obj, 5f);
    }
    
    
    public static void CreateFlame(Vector2 pos)
    {
        var obj = Object.Instantiate(FieldedObjects.Instance.ParticleFlame);
        Object.Destroy(obj, 5f);
    }
}