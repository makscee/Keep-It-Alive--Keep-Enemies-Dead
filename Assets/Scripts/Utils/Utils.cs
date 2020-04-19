using UnityEngine;

public static class Utils
{
    public static void DebugLogBy5(string s)
    {
        var lines = s.Split('\n');
        for (var i = 0; i < 5; i++)
            Debug.Log(i >= lines.Length ? "" : lines[i]);
    }

    public static Vector3 SetZ(this Vector3 v, float z)
    {
        return new Vector3(v.x, v.y, z);
    }
}