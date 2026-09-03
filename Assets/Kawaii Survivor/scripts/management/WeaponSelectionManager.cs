using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponSelectionManager : MonoBehaviour, IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private Transform containersParent;
    [SerializeField] private WeaponSelectionContainer weaponContainerPrefab;
    [SerializeField] private PlayerWeapons playerWeapons;

    [Header("Data")]
    [SerializeField] private WeaponDataSO[] starterWeapons;
    private WeaponDataSO selectedWeapon;
    private int initialWeaponLevel;

    void Start()
    {

    }

    void Update()
    {

    }

    public void GameStateChangedCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GAME:
                {
                    if(selectedWeapon==null)
                        return;

                    playerWeapons.AddWeapon(selectedWeapon , initialWeaponLevel);
                    selectedWeapon = null;
                    initialWeaponLevel = 0;
                    break;
                }
            case GameState.WEAPONSELECTION:
                {
                    Configure();
                    break;
                }
        }
    }

    [NaughtyAttributes.Button]
    private void Configure()
    {
        // DÜZELTME 2: Transform için Clear() metodu olmadığından, altındaki objeleri manuel siliyoruz.
        foreach (Transform child in containersParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < 3; i++)
        {
            GenerateWeaponContainer();
        }
    }

    private void GenerateWeaponContainer()
    {
        WeaponSelectionContainer containerInstance = Instantiate(weaponContainerPrefab, containersParent);

        WeaponDataSO weaponData = starterWeapons[Random.Range(0, starterWeapons.Length)];
        int Level = Random.Range(0, 4);

        containerInstance.Configure(weaponData.sprite, weaponData.Name, Level,weaponData);

        containerInstance.button.onClick.RemoveAllListeners();
        containerInstance.button.onClick.AddListener(() => weaponSelectedCallBack(containerInstance, weaponData , Level));
    }

    private void weaponSelectedCallBack(WeaponSelectionContainer containerInstance, WeaponDataSO weaponData , int Level)
    {
        initialWeaponLevel = Level;
        selectedWeapon = weaponData;

        foreach (WeaponSelectionContainer container in containersParent.GetComponentsInChildren<WeaponSelectionContainer>())
        {
            if (container == containerInstance)
            {
                container.Select();
            }
            else
            {
                container.Deselect();
            }
        }
    }
}