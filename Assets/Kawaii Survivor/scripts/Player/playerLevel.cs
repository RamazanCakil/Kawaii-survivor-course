using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;


public class playerLevel : MonoBehaviour
{

    [Header("Settings")]
    private int requiredXp;
    private int currentXp;
    private int level;


    [Header("visuals")]
    [SerializeField] private Slider xpBar;
    [SerializeField] private TextMeshProUGUI levelText;


    void Start()
    {
        updateRequiredXp();
        updateVisuals();

    }

    // Update is called once per frame
    void Update()
    {
    }
    private void Awake()
    {
                candy.onCollected += CandyCollectedCallback;
    }
    private void OnDestroy()
    {
        candy.onCollected -= CandyCollectedCallback;

    }

    private void updateRequiredXp() {

        requiredXp = (level + 1) * 5;
    }
    private void updateVisuals() {

        xpBar.value = (float) currentXp / requiredXp;
        levelText.text = "lvl" + (level+1);
    }

    private void CandyCollectedCallback(candy candy) {

        currentXp++;
        if (currentXp >= requiredXp)
            LevelUp();
        updateVisuals();
    }
    private void LevelUp() {
        level++;
        currentXp = 0;
        updateRequiredXp();
    }

}
