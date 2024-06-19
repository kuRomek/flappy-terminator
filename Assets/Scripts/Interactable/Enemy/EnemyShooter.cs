using System.Collections;
using UnityEngine;

public class EnemyShooter : Shooter
{
    [SerializeField] private Player _player;

    private Coroutine _shooting;
    private float _shootingRate = 1f;

    private void OnEnable()
    {
        _shooting = StartCoroutine(Shoot());
    }

    private void OnDisable()
    {
        StopCoroutine(_shooting);
    }

    private void Update()
    {
        if (IsGamePaused || transform.position.x < _player.transform.position.x)
            StopCoroutine(_shooting);
    }

    private IEnumerator Shoot()
    {
        WaitForSeconds shootingRate = new WaitForSeconds(_shootingRate);

        bool isRunning = true;

        while (isRunning)
        {
            yield return shootingRate;

            SetDirection(Vector2.left);
            ShootOneBullet();
        }
    }

    public void SetShootingRate(float rate)
    {
        _shootingRate = rate;
    }
}
