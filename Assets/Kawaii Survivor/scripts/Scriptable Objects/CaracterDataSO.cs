using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "CaracterDataSO", menuName = "Scriptable Objects/Caracter Data", order = 0)]
public class CaracterDataSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite sprite { get; private set; }
    [field: SerializeField] public int purchasePrice { get; private set; }

    [HorizontalLine]
    [SerializeField] private int attack;
    [SerializeField] private int attackSpeed;
    [SerializeField] private int criticalChance;
    [SerializeField] private int criticalPercent;
    [SerializeField] private int moveSpeed;
    [SerializeField] private int maxHealth;
    [SerializeField] private int range;
    [SerializeField] private int healthRecoverySpeed;
    [SerializeField] private int armor;
    [SerializeField] private int luck;
    [SerializeField] private int dodge;
    [SerializeField] private int lifesteal;


    public Dictionary<stat , float> BaseStats
    {  
        
        get
        {
            return new Dictionary<stat, float>
            {
                { stat.Attack,               attack },
                { stat.AttackSpeed,          attackSpeed },
                { stat.CriticalChance,       criticalChance },
                { stat.CriticalPercent,      criticalPercent },
                { stat.MoveSpeed,            moveSpeed },
                { stat.MaxHealth,            maxHealth },
                { stat.Range,                range },
                { stat.HealthRecoverySpeed,  healthRecoverySpeed },
                { stat.Armor,                armor },
                { stat.Luck,                 luck },
                { stat.Dodge,                dodge },
                { stat.LifeSteal,            lifesteal }
            };
        }




        //private set;
    }

}
