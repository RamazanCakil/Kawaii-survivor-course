using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponDataSO", menuName = "Scriptable Objects/Weapon Data", order = 0)]

public class WeaponDataSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite sprite { get; private set; }
    [field: SerializeField] public int purchasePrice { get; private set; }
    [field: SerializeField] public Boing Prefab { get; private set; }

    [HorizontalLine]
    [SerializeField] private int attack;
    [SerializeField] private int attackSpeed;
    [SerializeField] private int criticalChance;
    [SerializeField] private int criticalPercent;
    [SerializeField] private int range;



    public Dictionary<stat, float> BaseStats
    {

        get
        {
            return new Dictionary<stat, float>
            {
                { stat.Attack, attack },
                { stat.AttackSpeed, attackSpeed },
                { stat.CriticalChance, criticalChance },
                { stat.CriticalPercent, criticalPercent },
                { stat.Range, range }

            };
        }

    }
    public float GetStatValue(stat stat)
    {
        foreach (KeyValuePair<stat, float> kvp in BaseStats)
        {
            if (kvp.Key == stat)
            {
                return kvp.Value;
            }
        }
        return 0;
    }

}