using System.Collections;
using UnityEngine;

public abstract class DroppableCurrency : MonoBehaviour, Icollectable
{
    private bool collected;

    // BURASI DÜZELTİLDİ: Onable yerine OnEnable yazıldı!
    private void OnEnable()
    {
        collected = false;
    }

    public void Collect(Player player)
    {
        if (collected) return;

        collected = true;

        StartCoroutine(MoveTovardsPlayer(player));
    }

    IEnumerator MoveTovardsPlayer(Player player)
    {
        float timer = 0;
        Vector2 initialPosition = transform.position;

        while (timer < 1)
        {
            Vector2 targetPosition = player.GetCenter();
            transform.position = Vector2.Lerp(initialPosition, targetPosition, timer);
            timer += Time.deltaTime;
            yield return null;
        }
        Collected();
    }

    protected virtual void Collected()
    {
        gameObject.SetActive(false);
    }
}