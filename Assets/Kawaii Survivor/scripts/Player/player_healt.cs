using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class player_healt : MonoBehaviour
{
    [UnitHeaderInspectable("Settings")]
    [SerializeField] private int Maxhealth;
    private int health;

    [UnitHeaderInspectable("Elements")]
    [SerializeField] private Slider healtsSlider;
    [SerializeField] private TextMeshProUGUI healthText;



    void Start()
    {
        health = Maxhealth;
        UpdateUI();

    }

    public void TakeDamagee(int damage)
    {

        int realDamage = Mathf.Min(damage, health); 
        health -=realDamage;

        UpdateUI();

        Debug.Log("Health:" + health);

        if (health <= 0)
        {
            PassAway();
        }
    }
    private void PassAway()
    {
        GameManager.instance.setGameState(GameState.GAMEOVER);
        
    }

    private void UpdateUI() 
    {
        float healthBarValue = (float)health / Maxhealth;
        healtsSlider.value = healthBarValue;
        healthText.text = health + " / " + Maxhealth;
    }

}
