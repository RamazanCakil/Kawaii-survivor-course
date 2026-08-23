using UnityEngine;
using TMPro;
using UnityEngine.Pool;

public class DamageText : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshPro damageText;

    // YENİ: Havuz referansını hafızada tutacağız
    private ObjectPool<DamageText> myPool;

    // Obje üretildiğinde Manager ona havuzunu tanıtacak
    public void SetPool(ObjectPool<DamageText> pool)
    {
        myPool = pool;
    }

    public void DamageAnimation(int damage, bool isCriticalHit)
    {
        damageText.text = damage.ToString();
        damageText.color = isCriticalHit ? Color.yellow : Color.white;

        // ÇÖZÜM 1 (Görünmezlik Hatası): Animasyonu zorla -1 (herhangi bir katman) ve 0f (sıfırıncı saniye) ile en baştan başlatır!
        animator.Play("Animate", -1, 0f);

        // ÇÖZÜM 2 (T Harfi Hatası): LeanTween yerine kendi Invoke sistemimizle 1 saniye sonra havuza dönüyoruz
        CancelInvoke("ReturnToPool");
        Invoke("ReturnToPool", 1f);
    }

    void OnEnable()
    {
        // Obje her aktifleştiğinde animatörü en başa sar
        if (animator != null)
        {
            animator.Play("Animate", -1, 0f);
        }
    }

    private void ReturnToPool()
    {
        if (myPool != null && gameObject.activeSelf)
        {
            myPool.Release(this);
        }
    }
}