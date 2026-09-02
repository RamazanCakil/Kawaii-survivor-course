using UnityEngine;
using TMPro;
using UnityEngine.Pool;

public class DamageText : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshPro damageText;

    private ObjectPool<DamageText> myPool;

    public void SetPool(ObjectPool<DamageText> pool)
    {
        myPool = pool;
    }

    // BURAYI DEĞİŞTİRDİK: 'int damage' yerine 'string textContent' yaptık!
    public void DamageAnimation(string textContent, bool isCriticalHit)
    {
        // Gelen değer zaten metin (string) olduğu için ToString() silindi.
        damageText.text = textContent;
        damageText.color = isCriticalHit ? Color.yellow : Color.white;

        // Animasyonu en baştan başlat
        animator.Play("Animate", -1, 0f);

        // Havuza dönüş işlemini planla
        CancelInvoke("ReturnToPool");
        Invoke("ReturnToPool", 1f);
    }

    void OnEnable()
    {
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