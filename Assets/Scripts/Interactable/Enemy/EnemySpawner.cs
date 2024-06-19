using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawningRate = 3f;
    [SerializeField] private float _minSpawningRate = 0.8f;
    [SerializeField] private float _shootingRate = 0.8f;
    [SerializeField] private float _minShootingRate = 0.4f;
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private Player _player;
    [SerializeField] private AudioSource _audioSource;

    private float _defaultSpawningRate;
    private float _defaultShootingRate;
    private Collider2D _spawningArea;
    private Coroutine _spawning;

    public event Action<int> OnEnemyDied;
    public event Action<int> OnEnemyDisappeared;

    private void Awake()
    {
        _defaultSpawningRate = _spawningRate;
        _defaultShootingRate = _shootingRate;
        _spawningArea = GetComponent<Collider2D>();
    }

    private void Start()
    {
        _spawning = StartCoroutine(BeginSpawning());
    }

    private void FixedUpdate()
    {
        _spawningRate = Mathf.MoveTowards(_spawningRate, _minSpawningRate, _player.Mover.Acceleration * Time.fixedDeltaTime);
        _shootingRate = Mathf.MoveTowards(_shootingRate, _minShootingRate, _player.Mover.Acceleration * Time.fixedDeltaTime);
    }

    private IEnumerator BeginSpawning()
    {
        bool isRunning = true;

        while (isRunning)
        {
            yield return new WaitForSeconds(_spawningRate);

            Enemy newEnemy = _enemyPool.Get();
            newEnemy.transform.position = new Vector3(transform.position.x, UnityEngine.Random.Range(_spawningArea.bounds.min.y, _spawningArea.bounds.max.y), transform.position.z);
            newEnemy.Shooter.SetShootingRate(_shootingRate);
            newEnemy.OnDying += GetPointsForKill;
            newEnemy.OnDisappearing += GetPointsForSkip;
        }
    }

    private void GetPointsForKill(Enemy enemy)
    {
        enemy.OnDying -= GetPointsForKill;
        OnEnemyDied?.Invoke(2);
    }

    private void GetPointsForSkip(Enemy enemy)
    {
        enemy.OnDisappearing -= GetPointsForSkip;
        OnEnemyDisappeared?.Invoke(1);
    }

    public void Reset()
    {
        _shootingRate = _defaultShootingRate;
        _spawningRate = _defaultSpawningRate;

        if (_spawning != null)
            StopCoroutine(_spawning);

        _enemyPool.Reset();
        _spawning = StartCoroutine(BeginSpawning());
    }
}
