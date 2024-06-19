using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyEffects : MonoBehaviour
{
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _explosionSound;
    [SerializeField] private Animator _explosionEffect;

    private Enemy _enemy;
    private AudioSource _audioSource;

    public int Explode { get; } = Animator.StringToHash(nameof(Explode));

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _enemy.OnDying += CreateExplosion;
        _explosionEffect.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _enemy.OnDying -= CreateExplosion;
    }

    private void CreateExplosion(Enemy enemy)
    {
        _explosionEffect.gameObject.SetActive(true);
        _explosionEffect.SetTrigger(Explode);

        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.PlayOneShot(_explosionSound);
    }
}
