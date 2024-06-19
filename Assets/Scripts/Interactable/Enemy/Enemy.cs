using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyMover), typeof(EnemyShooter), typeof(EnemyCollisionHandler))]
public class Enemy : PooledObject, IInteractable
{
    [SerializeField] private Player _player;
    [SerializeField] private SpriteRenderer _sprite;

    private Collider2D _collider;
    private EnemyCollisionHandler _collisionHandler;
    private EnemyShooter _shooter;
    private EnemyMover _mover;

    public event Action<Enemy> OnDying;
    public event Action<Enemy> OnDisappearing;

    public EnemyShooter Shooter => _shooter;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collisionHandler = GetComponent<EnemyCollisionHandler>();
        _shooter = GetComponent<EnemyShooter>();
        _mover = GetComponent<EnemyMover>();
    }

    private void OnEnable()
    {
        Reset();
        _collisionHandler.OnCollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _collisionHandler.OnCollisionDetected -= ProcessCollision;
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Offscreen)
            Disappear();

        if (interactable is PlayerBullet)
            Die();
    }

    private void Disappear()
    {
        OnDisappearing?.Invoke(this);

        gameObject.SetActive(false);
    }

    private void Die()
    {
        OnDying?.Invoke(this);

        _sprite.gameObject.SetActive(false);
        _shooter.enabled = false;
        _mover.enabled = false;
        _collider.enabled = false;

        StartCoroutine(Dying());
    }

    private IEnumerator Dying()
    {
        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }

    public void Reset()
    {
        _sprite.gameObject.SetActive(true);
        _shooter.enabled = true;
        _shooter.Reset();
        _mover.enabled = true;
        _collider.enabled = true;
    }
}
