using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected float _maxHealth = 100f;
    protected float _currentHealth;

    public Action onUpdateHealth;

    protected virtual void OnEnable()
    {
        _currentHealth = _maxHealth;
        onUpdateHealth?.Invoke();
    }

    public virtual float GetHealth()
    {
        return _currentHealth;
    }

    public virtual float GetMaxHealth()
    {
        return _maxHealth;
    }

    public virtual void SetMaxHealth(float value)
    {
        _maxHealth = value;
        onUpdateHealth?.Invoke();
    }

    public virtual void ChangeHealth(float num)
    {
        _currentHealth += num;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        onUpdateHealth?.Invoke();
    }
}
