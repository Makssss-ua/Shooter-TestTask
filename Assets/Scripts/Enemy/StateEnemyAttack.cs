using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "StateEnemyAttack", menuName = "EnemyStates/StateEnemyAttack")]
public class StateEnemyAttack : EnemyState
{
    [SerializeField] private float _attackDistance = 1f;
    [SerializeField] private AnimationCurve _jumpCurve;

    private EnemySizeContoller _sizeController;
    private NavMeshAgent _agent;
    private EnemyPlayerDetector _playerDetector;
    private PlayerHealth _targetPlayer;
    private Animator _anim;

    public override void Init(NavMeshAgent agent, EnemyPlayerDetector playerDetector, EnemySizeContoller sizeContoller, Animator anim)
    {
        isFinished = false;
        _agent = agent;
        _anim = anim;
        _sizeController = sizeContoller;
        _attackDistance *= _sizeController.size;
        _playerDetector = playerDetector;
        _targetPlayer = playerDetector.player;
    }
    public override void Run()
    {
        if (isFinished)
            return;
        float distance = Vector3.Distance(_agent.transform.position, _targetPlayer.transform.position);
        if(distance < _playerDetector.viewDistance && _playerDetector.InViewRange(_targetPlayer.transform, _playerDetector.viewDistance))
        {
            _agent.SetDestination(_targetPlayer.transform.position);
        }
        else
        {
            if(_agent.velocity.z == 0)
            {
                isFinished = true;
            }
        }
        if(distance < _attackDistance && _playerDetector.seePlayer)
        {
            _anim.SetTrigger("Attack");
        }
    }
}
