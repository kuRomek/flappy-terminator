using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private float _bulletsOffset = 0.5f;
    [SerializeField] private AudioClip _shootSound;

    private AudioSource _audioSource;
    private Vector3 _direction;

    public bool IsGamePaused => Time.timeScale == 0f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    protected void ShootOneBullet()
    {
        Bullet bullet = _bulletPool.Get(_direction);
        bullet.transform.position = transform.position + _bulletsOffset * _direction;

        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.PlayOneShot(_shootSound);
    }

    public void SetDirection(Vector2 direction)
    {
        if (direction.x < 0)
        {
            if (direction.y < -0.5f)
            {
                direction.y = -0.5f;
                direction.x = -Mathf.Sqrt(3f) / 2f;
            }
            else if (direction.y > 0.5f)
            {
                direction.y = 0.5f;
                direction.x = -Mathf.Sqrt(3f) / 2f;
            }
        }

        _direction = direction.normalized;
    }

    public void Reset()
    {
        _bulletPool.Reset();
    }
}
