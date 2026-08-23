using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Elements")]
    private Rigidbody2D rig;
    private Collider2D col;

    private rangeweapon myWeapon;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask EnemyMask;
    private int damage;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    // YENİ: Mermi havuzdan her çıktığında eski saniye sayacını sıfırlar
    public void Reload()
    {
        CancelInvoke("Release");
    }

    public void Shoot(int damage, Vector2 direction, rangeweapon weapon)
    {
        this.damage = damage;
        this.myWeapon = weapon;

        transform.right = direction;
        rig.linearVelocity = direction * moveSpeed;

        // Eski iptal komutlarını temizle ve 1 saniye sonra yok ol
        CancelInvoke("Release");
        Invoke("Release", 1f);
    }

    private void Release()
    {
        CancelInvoke("Release");
        // Eğer silah yerindeyse mermiyi havuza geri gönder
        if (myWeapon != null && gameObject.activeSelf)
        {
            myWeapon.ReleaseBullet(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            Attack(enemy);
            Release();
        }
        else if (collision.TryGetComponent(out RangeEnemy rangeEnemy))
        {
            rangeEnemy.takeDamage(damage);
            Release();
        }
    }

    private void Attack(Enemy enemy)
    {
        enemy.takeDamage(damage);
    }
}