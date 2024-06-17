using System;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    public event Action<IInteractable> OnCollisionDetected; 

    private void OnTriggerEnter2D(Collider2D other)
    {
         if (other.TryGetComponent(out IInteractable interactable))
            OnCollisionDetected?.Invoke(interactable);
    }
}
