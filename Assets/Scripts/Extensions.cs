﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{

    public static void Kill(this GameObject obj)
    {
        Object.Destroy(obj);
    }


    public static Vector3 Unidirectional(this Vector3 dir)
    {
        var x = Mathf.Abs(dir.x);
        var y = Mathf.Abs(dir.y);
        var z = Mathf.Abs(dir.z);
        float d = 0;
        if (x > y && x > z)
        {
            d = dir.x;
            dir = new Vector3(1, 0, 0);
        }
        else if (y > x && y > z)
        {
            d = dir.y;
            dir = new Vector3(0, 1, 0);
        }
        else if (z > x && z > y)
        {
            d = dir.z;
            dir = new Vector3(0, 0, 1);
        }
        else
        {
            return Vector3.zero;
        }

        if (d < 0)
        {
            dir *= -1;
        }
        return dir;
    }

    public static Vector2 Unidirectional(this Vector2 dir)
    {
        var vec = new Vector3(dir.x, dir.y, 0).Unidirectional();
        return new Vector2(vec.x, vec.y);
    }

    public static Vector3 Directional(this Vector3 dir)
    {

        var x = 0f;
        var y = 0f;
        var z = 0f;
        if (dir.x != 0) x = dir.x / Mathf.Abs(dir.x);
        if (dir.y != 0) y = dir.y / Mathf.Abs(dir.y);
        if (dir.z != 0) z = dir.z / Mathf.Abs(dir.z);

        return new Vector3(x, y, z);

    }

    public static Vector2 Directional(this Vector2 dir)
    {
        var vec = new Vector3(dir.x, dir.y, 0).Directional();
        return new Vector2(vec.x, vec.y);
    }

}
