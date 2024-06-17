using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerMover), typeof(PlayerShooter), typeof(PlayerCollisionHandler))]
public class Player : MonoBehaviour  
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _explosionSound;

    private AudioSource _audioSource;
    private PlayerCollisionHandler _collisionHandler;
    private PlayerMover _mover;
    private PlayerShooter _shooter;
    
    public PlayerMover Mover => _mover;
    public PlayerShooter Shooter => _shooter;
    public ScoreCounter ScoreCounter => _scoreCounter;

    public event Action OnGameOver;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _collisionHandler = GetComponent<PlayerCollisionHandler>();
        _mover = GetComponent<PlayerMover>();
        _shooter = GetComponent<PlayerShooter>();
    }

    private void OnEnable()
    {
        _collisionHandler.OnCollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _collisionHandler.OnCollisionDetected -= ProcessCollision;
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Ground || interactable is Offscreen || interactable is EnemyBullet || interactable is Enemy)
        {
            _audioSource.PlayOneShot(_explosionSound);
            OnGameOver?.Invoke();
        }
    }

    public void Reset()
    {
        _mover.Reset();
        _shooter.Reset();
        _scoreCounter.Reset();
    }
}
