using System;
using UnityEngine;

public class Bullet : PooledObject, IInteractable
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private float _speed = 7.5f;

    private Vector2 _direction;

    public event Action<Bullet> OnCollisionDetected;

    private void FixedUpdate()
    {
        transform.Translate((_playerMover.Speed * Vector2.right + _speed * _direction) * Time.fixedDeltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        OnCollisionDetected?.Invoke(this);
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }
}
