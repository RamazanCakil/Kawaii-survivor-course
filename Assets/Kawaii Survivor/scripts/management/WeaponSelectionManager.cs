using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponSelectionManager : MonoBehaviour, IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private Transform containersParent;
    [SerializeField] private WeaponSelectionContainer weaponContainerPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
            case GameState.WEAPONSELECTİON:
                {
                    Configure();
                    break;

                }
        }


    }

    private void Configure() {

        containersParent.Clear();

        for (int i=0;i<3;i++)
        {
            GenerateWeaponContainer();
        }
    }

    private void GenerateWeaponContainer()
    {
        WeaponSelectionContainer containerInstance = Instantiate(weaponContainerPrefab, containersParent);

    }

}

