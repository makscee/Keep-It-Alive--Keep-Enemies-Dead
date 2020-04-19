using System;
using UnityEngine;
using Random = UnityEngine.Random;

public struct Template
{
    public string shape;
    public Color color;
}
public class ShapeTemplates
{
    static readonly string[] Templates = new[]
    {
        "0   " +
        "0   " +
        "0   " +
        "0   ",
        
        "    " +
        "    " +
        "0   " +
        "000 ",
        
        "    " +
        "    " +
        "  0 " +
        "000 ",
        
        "    " +
        "    " +
        "00  " +
        "00  ",
        
        "    " +
        "    " +
        " 0  " +
        "000 ",
        
        "    " +
        "    " +
        " 00 " +
        "00  ",
        
        "    " +
        "    " +
        "00  " +
        " 00 ",
        
        "    " +
        "    " +
        "0   " +
        "00  ",
        
        "    " +
        "    " +
        "00  " +
        " 0  ",
        
        "    " +
        "    " +
        "000 " +
        "  0 ",
        
        "    " +
        "    " +
        "    " +
        "0000",
        
        "    " +
        "0   " +
        "0   " +
        "00  ",
        
        "    " +
        "    " +
        "0   " +
        "00  ",
        
        "    " +
        "    " +
        "    " +
        "00  ",
        
        "    " +
        "    " +
        "0   " +
        "0   ",
        
        "    " +
        "    " +
        "    " +
        "0   ",
        
        "    " +
        "0   " +
        "00  " +
        "0   ",
        
        "    " +
        " 0  " +
        "00  " +
        " 0  ",
    };

    public static Template GetRandom()
    {
        var ind = Random.Range(0, Templates.Length);
        var c = Color.HSVToRGB((float) ind / Templates.Length, 0.6f, 0.8f);
        var t = new Template
        {
            shape = Templates[ind],
            color = c
        };
        return t;
    }
}
