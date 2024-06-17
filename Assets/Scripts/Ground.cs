using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Ground : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerMover _player;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _animator.speed += _player.Acceleration * Time.fixedDeltaTime;
    }
}