using UnityEngine;

public class PlayerShooter : Shooter
{
    private const string Fire1 = nameof(Fire1);

    [SerializeField] private float _damage = 5f;

    public float Damage => _damage;

    private void Start()
    {
        SetDirection(Vector2.right);
    }

    private void Update()
    {
        if (Input.GetButtonDown(Fire1) && IsGamePaused == false)
            ShootOneBullet();
    }
}
