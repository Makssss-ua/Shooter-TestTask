using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyPlayerDetector _playerDetector;
    [SerializeField] private EnemySizeContoller _sizeContoller;
    [SerializeField] private Vector3 _patrolCenter;
    [SerializeField] private Animator _anim;
    [SerializeField] private EnemyHealth _enemyHealth;

    [Space(5f)]
    [Header("States")]
    [SerializeField] private StateEnemyPatrol _patrolState;
    [SerializeField] private StateEnemyAttack _attackState;
    [Space(2f)]
    [Header("Active State")]
    [SerializeField] private EnemyState _curentState;

    private void Start()
    {
        _curentState = Instantiate(_patrolState);
        _curentState.Init(_agent, _patrolCenter, _enemyHealth);
    }

    private void FixedUpdate()
    {
        AnimLocomotion();
        if (_playerDetector.seePlayer)
        {
            _curentState = Instantiate(_attackState);
            _curentState.Init(_agent, _playerDetector, _sizeContoller, _anim);
        }
        if (_curentState.isFinished)
        {
            _curentState = Instantiate(_patrolState);
            _curentState.Init(_agent, _patrolCenter, _enemyHealth);
        }
        else
        {
            _curentState.Run();
        }
    }

    private void AnimLocomotion()
    {
        float speed = Mathf.Clamp(Mathf.Abs(_agent.velocity.x) + Mathf.Abs(_agent.velocity.z), 0f, 1f);
        if (speed > 0.05f)
        {
            _anim.SetBool("Move", true);
            _anim.SetFloat("speed", speed);
        }
        else
        {
            _anim.SetBool("Move", false);
        }
    }
}
