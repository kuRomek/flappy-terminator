using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyMover), typeof(EnemyShooter), typeof(EnemyCollisionHandler))]
public class Enemy : PooledObject, IInteractable
{
    [SerializeField] private HealthPoints _health;
    [SerializeField] private Player _player;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _explosionSound;
    [SerializeField] private Animator _explosionEffect;
    [SerializeField] private SpriteRenderer _sprite;

    private Collider2D _collider;
    private AudioSource _audioSource;
    private EnemyCollisionHandler _collisionHandler;
    private EnemyShooter _shooter;
    private EnemyMover _mover;
    private int Explode = Animator.StringToHash(nameof(Explode));

    public event Action<Enemy> OnDying;

    public EnemyShooter Shooter => _shooter;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _collider = GetComponent<Collider2D>();
        _collisionHandler = GetComponent<EnemyCollisionHandler>();
        _shooter = GetComponent<EnemyShooter>();
        _mover = GetComponent<EnemyMover>();
    }

    private void OnEnable()
    {
        Reset();
        _health.OnDropedToZero += Die;
        _collisionHandler.OnCollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _health.OnDropedToZero -= Die;
        _collisionHandler.OnCollisionDetected -= ProcessCollision;
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is PlayerBullet)
        {
            _audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
            _audioSource.PlayOneShot(_hitSound);
            _health.TakeDamage(_player.Shooter.Damage);
        }

        if (interactable is Offscreen)
        {
            gameObject.SetActive(false);
            _player.ScoreCounter.GetPoints(1);
        }
    }

    private void Die()
    {
        _player.ScoreCounter.GetPoints(2);

        _explosionEffect.gameObject.SetActive(true);
        _explosionEffect.SetTrigger(Explode);

        _audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        _audioSource.PlayOneShot(_explosionSound);

        _sprite.gameObject.SetActive(false);
        _shooter.enabled = false;
        _mover.enabled = false;
        _collider.enabled = false;

        OnDying?.Invoke(this);

        StartCoroutine(Dying());
    }

    private IEnumerator Dying()
    {
        yield return new WaitForSeconds(0.5f);

        _explosionEffect.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void Reset()
    {
        _explosionEffect.gameObject.SetActive(false);

        _health.Reset();
        _sprite.gameObject.SetActive(true);
        _shooter.enabled = true;
        _mover.enabled = true;
        _collider.enabled = true;
    }
}
