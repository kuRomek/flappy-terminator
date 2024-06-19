using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private PlayerMover _player;

    private float _speed;

    private void Start()
    {
        _speed = _player.Speed * 0.85f;
    }

    private void FixedUpdate()
    {
        transform.Translate(_speed * Time.fixedDeltaTime * Vector3.right, Space.World);
    }
}
