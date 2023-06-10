using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHealth : MonoBehaviour
{
    [SerializeField] private ActiveContent _activeContent;
    [SerializeField] private Health _health;
    private Transform _transform;

    private void OnEnable()
    {
        _transform = transform;
        _health.onUpdateHealth += HealthUpdate;
    }

    private void Start()
    {
        HealthUpdate();
    }

    private void OnDisable()
    {
        _health.onUpdateHealth -= HealthUpdate;
    }

    private void HealthUpdate()
    {
        float activePointsCount = _health.GetHealth() / _health.GetMaxHealth();
        _activeContent.SetActivePoints(Mathf.RoundToInt(activePointsCount * _activeContent.healthPointsCount));
        if(_health.GetHealth() == 0)
            _activeContent.background.enabled = false;
    }
}
