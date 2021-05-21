using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomValueGenerator
{
    public static int RandomInt(int min, int max)
    {
        return Random.Range(min, max);
    }
}
