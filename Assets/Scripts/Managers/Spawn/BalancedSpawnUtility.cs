using UnityEngine;

public static class BalancedSpawnUtility
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

        for (int i = 0; i < wd.Weights.Length; i++)
        {
            if ((rand - wd.Weights[i]) <= 0)
            {
                return i;
            }
            else
            {
                rand -= wd.Weights[i];
            }
        }
        return 0;
    }
}
