using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class EnemyHealth : Health
{
    [SerializeField] private EnemyPlayerDetector _playerDetector;
    [SerializeField] private EnemyMove _enemyMove;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemySizeContoller _sizeController;
    [SerializeField] private Animator _anim;

    public Action onTakeDamage;
    public Vector3 lastShotPoint;

    protected override void OnEnable()
    {
        base.OnEnable();
        onUpdateHealth += CheckHealth;
    }

    private void OnDisable()
    {
        if (!gameObject.scene.isLoaded)
            return;
        onUpdateHealth -= CheckHealth;
    }

    public virtual void ChangeHealth(float num, Vector3 shotPoint)
    {
        base.ChangeHealth(num);
        lastShotPoint = shotPoint;
        onTakeDamage?.Invoke();
    }

    private void CheckHealth()
    {
        if (_currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        _anim.SetTrigger("Die");
        _agent.enabled = false;
        _playerDetector.enabled = false;
        _enemyMove.enabled = false;
        StartCoroutine(_sizeController.Die(transform));
    }

    private void OnDestroy()
    {
        if(GetHealth() <= 0)
            PlayerPrefs.SetInt("Player Kills", PlayerPrefs.GetInt("Player Kills") + 1);
    }
}
