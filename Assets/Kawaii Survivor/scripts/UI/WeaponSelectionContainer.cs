using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic; // EKLENDİ

public class WeaponSelectionContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;

    [Header("color")]
    [SerializeField] private Image[] levelDependentImages;

    [Header("stats")]
    [SerializeField] private Transform statContainersParent;
    [SerializeField] private StatContainer statContainerPrefab;

    [field: SerializeField] public Button button { get; private set; }

    public void Configure(Sprite sprite, string name, int level, WeaponDataSO weaponData)
    {
        icon.sprite = sprite;
        nameText.text = name + $" (lvl {level + 1})";

        Color imageColor = ColorHolder.GetColor(level);
        nameText.color = imageColor;

        foreach (Image image in levelDependentImages)
        {
            image.color = imageColor;
        }

        Dictionary<stat, float> calculatedStats = WeaponStatsCalculator.GetStats(weaponData, level);
        ConfigureStatContainer(calculatedStats);
    }

    private void ConfigureStatContainer(Dictionary<stat, float> calculatedStats)
    {
        StatContainerManager.GenerateStatContainers(calculatedStats, statContainersParent);

    }

    public void Select()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one * 1.075f, .3f).setEase(LeanTweenType.easeInOutSine);
    }

    public void Deselect()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one, .3f);
    }
}