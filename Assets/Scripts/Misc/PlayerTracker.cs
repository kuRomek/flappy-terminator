using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private Player _player;

    private float _offset = 1.72f;

    private void FixedUpdate()
    {
        Vector3 position = transform.position;
        position.x = _player.transform.position.x + _offset;
        transform.position = position;
    }
}
