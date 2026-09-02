using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Linq;

public class PlayerStatsManager : MonoBehaviour
{
    [Header(" Data ")]
    [SerializeField] private CaracterDataSO PlayerData;


    [Header("Settings")]
    private Dictionary<stat , float> playerStats = new Dictionary<stat, float>();
    private Dictionary<stat , float> addends = new Dictionary<stat, float>();


    private void Awake()
    {
        playerStats = PlayerData.BaseStats;

        foreach (KeyValuePair<stat, float> kvp in playerStats)
        {
            addends.Add(kvp.Key , 0);
        }


    }
    void Start()
    { 

        UpdatePlayerStats();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddPlayerStat(stat stat , float value)
    {

        if(addends.ContainsKey(stat))
        {
            addends[stat] += value;
        }
        else
        {
            
        }
        UpdatePlayerStats();

    }


    public float GetStatValue(stat stat)
    {
        float value = playerStats[stat] + addends[stat];
        return value;
    }


    private void UpdatePlayerStats()
    {
        IEnumerable<IPlayerStatsDependency> playerStatsDependencies = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).OfType<IPlayerStatsDependency>();

        foreach (IPlayerStatsDependency dependency in playerStatsDependencies)
        {
            // 's' takısı kaldırılarak koleksiyon yerine tekil eleman üzerinden metot çağrıldı.
            dependency.UpdateStats(this);
        }

        
    }


}

struct StatData
{
    public stat stat;
    public float value;

    public StatData (stat stat , float value)
    {
        this.stat = stat;
        this.value = value;
    }
}
