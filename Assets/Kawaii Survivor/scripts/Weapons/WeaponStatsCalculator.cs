using System.Collections.Generic;
using UnityEngine;

public static class WeaponStatsCalculator
{
    public static Dictionary<stat, float> GetStats(WeaponDataSO weaponData, int level)
    {
        float multiplier = 1 + (float)level / 3;

        Dictionary<stat, float> calculatedStats = new Dictionary<stat, float>();

        foreach (KeyValuePair<stat, float> kvp in weaponData.BaseStats)
        {
            if (weaponData.Prefab.GetType() != typeof(rangeweapon) && kvp.Key == stat.Range)
                calculatedStats.Add(kvp.Key, kvp.Value);
            else
                calculatedStats.Add(kvp.Key, kvp.Value * multiplier);
        }
        return calculatedStats;
    }
}