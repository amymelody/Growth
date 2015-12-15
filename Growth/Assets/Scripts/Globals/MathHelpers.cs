using UnityEngine;
using System.Collections;

public class MathHelpers
{
    public static float QuadraticInterpolation(float a, float b, float t)
    {
        if (t > 1)
            t = 1;
        if (t < 0)
            t = 0;
        return a + (b - a) * t * t;
    }
}
