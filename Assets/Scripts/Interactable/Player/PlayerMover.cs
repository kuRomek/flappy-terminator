using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const string Jump = nameof(Jump);

    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _acceleration = 0.3f;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;
    [SerializeField] private float _rotationSpeed;

    private Rigidbody2D _rigidbody;
    private Vector2 _startingPosition;
    private float _startingSpeed;
    private bool _isReadyToJump = false;
    private Quaternion _minRotation;
    private Quaternion _maxRotation;
    private Quaternion _targetRotation;

    public float Speed => _speed;
    public float Acceleration => _acceleration;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _startingSpeed = _speed;
    }

    private void Start()
    {
        _startingPosition = transform.position;

        _minRotation = Quaternion.Euler(0f, 0f, _minRotationZ);
        _maxRotation = Quaternion.Euler(0f, 0f, _maxRotationZ);
    }

    private void Update()
    {
        if (Input.GetButtonDown(Jump))
            _isReadyToJump = true;
    }

    private void FixedUpdate()
    {
        _speed += _acceleration * Time.fixedDeltaTime;

        if (_isReadyToJump)
            GainHeight();

        Move();
        Rotate();
    }

    private void Move()
    {
        ChangeVelocity(_speed, _rigidbody.velocity.y);
    }

    private void GainHeight()
    {
        ChangeVelocity(_rigidbody.velocity.x, _jumpForce);
        _isReadyToJump = false;
    }

    private void Rotate()
    {
        _targetRotation = _rigidbody.velocity.y > 0f ? _maxRotation : _minRotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, Mathf.Abs(_rigidbody.velocity.y) / 40f);
    }

    private void ChangeVelocity(float x, float y)
    {
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = x;
        velocity.y = y;
        _rigidbody.velocity = velocity;
    }

    public void Reset()
    {
        transform.SetPositionAndRotation(_startingPosition, Quaternion.identity);

        Vector2 velocity = _rigidbody.velocity;
        velocity.y = _jumpForce;
        _rigidbody.velocity = velocity;

        _speed = _startingSpeed;
    }
}
