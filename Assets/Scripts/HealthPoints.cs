using System;
using UnityEngine;

public class HealthPoints : MonoBehaviour
{
    [SerializeField] private float _max;

    private float _amount;

    public event Action OnValueChanged;
    public event Action OnDropedToZero;

    public float Max => _max;
    public float Amount => _amount;

    private void Awake()
    {
        _amount = _max;
    }

    public void TakeDamage(float value)
    {
        if (value >= 0f)
        {
            _amount = Mathf.Clamp(_amount - value, 0f, _max);
            OnValueChanged?.Invoke();

            if (_amount == 0f)
                OnDropedToZero?.Invoke();
        }
        else
        {
            throw new ArgumentException("Argument cannot be less then 0");
        }
    }

    public void Heal(float value)
    {
        if (value >= 0f)
        {
            _amount = Mathf.Clamp(_amount + value, 0f, _max);
            OnValueChanged?.Invoke();
        }
        else
        {
            throw new ArgumentException("Argument cannot be less then 0");
        }
    }

    public void Reset()
    {
        _amount = _max;
    }
}
