using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using System;
using NaughtyAttributes;

public class WaveTransitionManager : MonoBehaviour, IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private Button[] upgradeContainers;

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

            upgradeContainers[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Upgrade" + i;

            upgradeContainers[i].onClick.RemoveAllListeners();

        }
    
    }

}
