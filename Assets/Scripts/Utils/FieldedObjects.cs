using UnityEngine;
using UnityEngine.UI;

public class FieldedObjects : MonoBehaviour
{
    // prefabs
    public GameObject TetrisFieldBlock, ShapeBlock, ShapeShadowBlock, HpText, DmgText,
        Enemy, Pixel, ParticleExplosion, ParticleFlame;
    
    public Camera Camera;
    public Canvas Canvas, GameOverCanvas;
    public Text GameOverText;

    public static FieldedObjects Instance;

    void OnEnable()
    {
        Instance = this;
    }
}