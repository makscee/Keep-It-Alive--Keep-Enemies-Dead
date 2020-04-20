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
            Enemy.addDmg = 0;
            Enemy.addHp = 0;
            Enemy.Kills = 0;
            TetrisField.LinesCleared = 0;
            SceneManager.LoadScene("Game");
        }

        // if (Input.GetKeyDown(KeyCode.D))
        // {
        //     DmgText.Create(14, Player.Instance.transform.position);
        // }
    }
}