using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;
public class player_healt : MonoBehaviour ,IPlayerStatsDependency
{
    [UnitHeaderInspectable("Settings")]
    [SerializeField] private int baseMaxhealth;
    private int maxHealth;
    private float health;
    private float armor;
    private float lifeSteal;
    private float dodge;
    private float healthRecoverySpeed;
    private float healthRecoveryValue;
    private float healthRecoveryTimer;
    private float healthRecoveryDuration;

    

    [UnitHeaderInspectable("Elements")]
    [SerializeField] private Slider healtsSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    [UnitHeaderInspectable(" Actions ")]
    public static Action <Vector2> onAttackDodged;


    private void Awake()
    {
        Enemy.onDamageTaken += EnemyTookDamageCallBack;
    }
    private void OnDestroy()
    {
        Enemy.onDamageTaken -= EnemyTookDamageCallBack;
    }

    private void EnemyTookDamageCallBack(int damage, Vector2 enemyPose, bool isCritical)
    {
        if (health >= maxHealth)
            return;

        float lifeStealValue = damage * lifeSteal;
        float healthToAdd = Mathf.Min(lifeStealValue, maxHealth - health);
        health += healthToAdd;
        UpdateUI();

    }

    void Start()
    {


    }
    private void Update()
    {
        if (health < maxHealth)
        {
            RecoverHealth();
        }
    }

    private void RecoverHealth()
    {
        healthRecoveryTimer += Time.deltaTime;
        
        if(healthRecoveryTimer > healthRecoveryDuration)
        {
            healthRecoveryTimer = 0;
            float healthToAdd = Mathf.Min(.1f, maxHealth , health);
            health += healthToAdd;

            UpdateUI();
        }

    }
    public void TakeDamagee(int damage)
    {
        if (shouldDodge())
        {
            onAttackDodged?.Invoke(transform.position);
            return;
        }

        float realDamage = damage * Mathf.Clamp(1- (armor/1000) , 0 , 10000);
        realDamage = Mathf.Min(realDamage, health); 
        health -=realDamage;

        UpdateUI();

        Debug.Log("Health:" + health);

        if (health <= 0)
        {
            PassAway();
        }
    }

    private bool shouldDodge()
    {
        return Random.Range(0f, 100f) < dodge;
    }
    private void PassAway()
    {
        GameManager.instance.setGameState(GameState.GAMEOVER);
        
    }

    private void UpdateUI() 
    {
        float healthBarValue = health / baseMaxhealth;
        healtsSlider.value = healthBarValue;
        healthText.text = (int)health + " / " + baseMaxhealth;
    }

    public void UpdateStats(PlayerStatsManager playerStatsManager)
    {
        float addHealth = playerStatsManager.GetStatValue(stat.MaxHealth);
        maxHealth = baseMaxhealth + (int)addHealth;
        maxHealth = Mathf.Max(maxHealth, 1);

        health = maxHealth;
        UpdateUI();

        armor = playerStatsManager.GetStatValue(stat.Armor);
        lifeSteal = playerStatsManager.GetStatValue(stat.LifeSteal) / 100;
        dodge = playerStatsManager.GetStatValue(stat.Dodge);

        healthRecoverySpeed = Mathf.Max(.0001f , playerStatsManager.GetStatValue(stat.HealthRecoverySpeed));
        healthRecoveryDuration = 1f / healthRecoverySpeed;

    }
}
