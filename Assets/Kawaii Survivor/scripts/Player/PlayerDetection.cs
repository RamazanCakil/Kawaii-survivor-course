using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerDetection : MonoBehaviour
{
    [Header("Colliders")]
    [SerializeField] private CircleCollider2D collactableCollider;


    /*
     
    private void FixedUpdate()
    {
        Collider2D[] candyColliders = Physics2D.OverlapCircleAll((Vector2)transform.position + daveCollider.offset, daveCollider.radius);

        foreach (Collider2D collider in candyColliders)
        {

            if (collider.TryGetComponent(out candy Candy))
            {
                Destroy(Candy);

            }

        }
    }

    */

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.TryGetComponent(out Icollectable collectable))
        {
            if (!collider.IsTouching(collactableCollider))
                return;

            collectable.Collect(GetComponent<Player>());

        }

       

    }
    
}
