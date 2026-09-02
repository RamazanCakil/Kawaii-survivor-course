using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using System;
using NaughtyAttributes;

public class WaveTransitionManager : MonoBehaviour, IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private UpgradeContainer[] upgradeContainers;
    [SerializeField] private PlayerStatsManager playerStatsManager;

    // ... kodun geri kalanı aynı
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStateChangedCallBack(GameState gameState) 
    {
        switch (gameState)
        {
            case GameState.WAVETRANSITION:
                ConfigureUpgradeContainers(); 
                break;


        }
    }
    [Button]
    private void ConfigureUpgradeContainers() 
    {
        for(int i = 0; i < upgradeContainers.Length; i++)
        {

            int randomIndex = Random.Range(0, Enum.GetValues(typeof(stat)).Length);
            stat stat = (stat)Enum.GetValues(typeof(stat)).GetValue(randomIndex);
            string randomStatString = enums.FormatStatName(stat);


            string buttonString;
            Action action = GetActionToPerform(stat, out buttonString);

            upgradeContainers[i].Configure(null,randomStatString,buttonString);

            upgradeContainers[i].button.onClick.RemoveAllListeners();
            upgradeContainers[i].button.onClick.AddListener(() => action?.Invoke());
            upgradeContainers[i].button.onClick.AddListener(() => BonusSelectedCallBack());


        }
    
    }

    private void BonusSelectedCallBack()
    {
        GameManager.instance.WaveComplateCallback();
    }

    private Action GetActionToPerform (stat stat , out string buttonString)
    {
        buttonString = "";
        float value;


        switch (stat)
        {
            case stat.Attack:
                value = Random.Range(1, 10);
                buttonString = "+" + value.ToString() + "%";
                break;

            case stat.AttackSpeed:
                value = Random.Range(1, 10);
                buttonString = "+" + value.ToString() + "%";
                break;

            case stat.CriticalChance:
                value = Random.Range(1, 10);
                buttonString = "+" + value.ToString() + "%";
                break;

            case stat.CriticalPercent:
                value = Random.Range(1f, 2f);
                buttonString = "+" + value.ToString("F2") + "x";
                break;

               case stat.MoveSpeed:
                value = Random.Range(1, 10);
                buttonString = "+" + value.ToString() + "%";
                break;

               case stat.MaxHealth:
                value = Random.Range(1, 5);
                buttonString = "+" + value;
                break;

               case stat.Range:
                value = Random.Range(1f, 5f);
                buttonString = "+" + value.ToString("F2");
                break;

               case stat.HealthRecoverySpeed:
                value = Random.Range(1, 10);
                buttonString = "+" + value.ToString() + "%";
                break;

               case stat.Armor:
                value = Random.Range(1, 10);
                buttonString = "+" + value.ToString() + "%";
                break;

               case stat.Luck:
                value = Random.Range(1, 10);
                buttonString = "+" + value.ToString() + "%";
                break;

               case stat.Dodge: 
                value = Random.Range(1, 10);
                buttonString = "+" + value.ToString() + "%";
                break;

            case stat.LifeSteal:
                value = Random.Range(1, 10);
                buttonString = "+" + value.ToString() + "%";
                break;




            default:
                return () => Debug.Log("Invalid Start");

        }

        return () => playerStatsManager.AddPlayerStat(stat,value);

    }

}
