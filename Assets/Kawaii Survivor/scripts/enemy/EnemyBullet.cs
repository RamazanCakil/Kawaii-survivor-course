using NaughtyAttributes;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D),typeof(Rigidbody2D))]
public class EnemyBullet : MonoBehaviour
{
    [UnitHeaderInspectable ("Elements")]
    private Rigidbody2D rig;
    private Collider2D Collider;
    private RangeEnemyAttack rangeEnemyAttack;

    [UnitHeaderInspectable("Settings")]
    private int damage;
    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
        LeanTween.delayedCall(gameObject, 5, () => rangeEnemyAttack.ReleaseBullet(this));

    }

    void Start()
    {
        
    }
    void Update()
    {

    }
    

    public void Configure(RangeEnemyAttack rangeEnemyAttack)
    {
        this.rangeEnemyAttack = rangeEnemyAttack;
    }

    public void shoot(int damage, Vector2 direction)
    {
        this.damage = damage;
        transform.right = direction;
        rig.linearVelocity = direction * moveSpeed;

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.TryGetComponent(out Player player))
        {
            LeanTween.cancel(gameObject);

            player.TakeDamage(damage);
            this.Collider.enabled = false;
            rangeEnemyAttack.ReleaseBullet(this);
        }
    }
    public void reload() {
        
        rig.linearVelocity=Vector2.zero;
        Collider.enabled = true;
    }

}
