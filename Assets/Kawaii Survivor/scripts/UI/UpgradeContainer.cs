using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UpgradeContainer : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Image image; 
    [SerializeField] private TextMeshProUGUI UpgradeNameText; 
    [SerializeField] private TextMeshProUGUI UpgradeValueText;

    [field: SerializeField]public Button button { get; private set; }


    public void Configure(Sprite icon, string upgradeName, string upgradeValue) 
    {
        image.sprite = icon;
        UpgradeNameText.text = upgradeName;
        UpgradeValueText.text = upgradeValue;
    }



}
