
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object Data", menuName = "Scriptable Objects/New Object", order = 0)]
public class ObjectDataSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public int Price { get; private set; }

    [field: Range(0, 3)]
    [field: SerializeField] public int Rarity { get; private set; }

    [field: SerializeField] private StatData[] statDatas;

    public Dictionary<stat, float> BaseStats
    {
        get
        {
            Dictionary<stat, float> stats = new Dictionary<stat, float>();

            foreach (StatData data in statDatas)
                stats.Add(data.stat, data.value);

            return stats;
        }
    }
}

[System.Serializable]
public struct StatDatas
{
    public stat Stat;
    public float Value;
}

