using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class DamageText : MonoBehaviour
{

    [UnitHeaderInspectable("Elements")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshPro damageText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    //[NaughtyAttributes.Button]
    public void DamageAnimation(int damage)
    { 
        damageText.text =damage.ToString();
        animator.Play("Animate");
        
        
    }


}
