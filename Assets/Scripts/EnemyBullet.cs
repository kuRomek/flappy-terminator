using UnityEngine;

public class EnemyBullet : Bullet 
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (other.TryGetComponent(out IInteractable interactable) && interactable is not Bullet && interactable is not Enemy)
            gameObject.SetActive(false);
    }
}
