using UnityEngine;
using System;
using System.Linq;
using Random = UnityEngine.Random;

public static class WeightedSpawnUtility
{


    public static int ReturnRandomChoice(SpawnData[] sd)
    {
        float rand = Random.Range(0f, 100f);

        for (int i = 0; i < sd.Length; i++)
        {
            if ((rand - sd[i].Weight) <= 0)
            {
                return i;
            }
            else
            {
                rand -= sd[i].Weight;
            }
        }
        return 0;
    }

    public static int ReturnWaveRandomIndex(WaveData wd)
    {
        float rand = Random.Range(0f, 100f);
        rand = 100 - rand;

        float[] results = new float[wd.Weights.Length];

        for (int i = 0; i < results.Length; i++)
        {
            results[i] = Mathf.Abs(rand - wd.Weights[i]);
        }
        return Array.IndexOf(results,results.Min());

    }
}
