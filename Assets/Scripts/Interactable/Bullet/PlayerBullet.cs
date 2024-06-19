using UnityEngine;

public class PlayerBullet : Bullet 
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (other.TryGetComponent(out IInteractable interactable) && interactable is not Bullet && interactable is not Player)
            gameObject.SetActive(false);
    }
}
