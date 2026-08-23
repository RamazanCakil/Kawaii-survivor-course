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
    private bool iscriticalhit;

    // Eğitmenin mantığı: Aynı anda iki kişiye vurmayı engellemek için hedefi aklında tut
    private Enemy target;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    // YENİ: Eğitmenin Reload mantığı buraya eklendi
    public void Reload()
    {
        target = null;                      // Hafızadaki hedefi sil ki yeni atışta başkasına vurabilsin
        rig.linearVelocity = Vector2.zero;  // Merminin eski hızını sıfırla
        col.enabled = true;                 // Merminin çarpışma algılayıcısını tekrar aç

        CancelInvoke("Release");
    }

    public void Shoot(int damage, Vector2 direction, rangeweapon weapon , bool iscriticalhit)
    {
        this.damage = damage;
        this.myWeapon = weapon;

        transform.right = direction;
        rig.linearVelocity = direction * moveSpeed;

        this.iscriticalhit = iscriticalhit;

        CancelInvoke("Release");
        Invoke("Release", 1f);
    }

    private void Release()
    {
        CancelInvoke("Release");
        if (myWeapon != null && gameObject.activeSelf)
        {
            myWeapon.ReleaseBullet(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // EĞİTMENİN GÜVENLİK KİLİDİ: Eğer mermi zaten birine çarptıysa (target boş değilse) aşağı inme, geri dön.
        if (target != null)
        {
            return;
        }

        // 1. İhtimal: Çarptığımız şey normal bir Enemy ise
        if (collision.TryGetComponent(out Enemy enemy))
        {
            target = enemy;      // Hedefi hafızaya al
            col.enabled = false; // Mermiyi kör et (başka şeye çarpmasın)

            Attack(enemy);
            Release();
        }
        // 2. İhtimal: Çarptığımız şey bir RangeEnemy ise (Gelecek hataları önlemek için)
        else if (collision.TryGetComponent(out RangeEnemy rangeEnemy))
        {
            col.enabled = false; // Mermiyi kör et
            rangeEnemy.takeDamage(damage,iscriticalhit);
            Release();
        }
    }

    private void Attack(Enemy enemy)
    {
        enemy.takeDamage(damage,iscriticalhit);
    }
}