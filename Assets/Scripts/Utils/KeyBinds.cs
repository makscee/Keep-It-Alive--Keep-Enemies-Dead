using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyBinds : MonoBehaviour
{
    void Update()
    {
        if (!Input.anyKeyDown) return;

        // if (Input.GetKeyDown(KeyCode.S))
        // {
        //     Destroy(Shape.HeldShape);
        //     var shape = ShapeConstructor.GetRandom();
        //     shape.mouseAttached = true;
        // }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Game");
        }

        // if (Input.GetKeyDown(KeyCode.D))
        // {
        //     DmgText.Create(14, Player.Instance.transform.position);
        // }
    }
}