using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "EnemyStatePatrol", menuName = "EnemyStates/EnemyStatePatrol")]
public class StateEnemyPatrol : EnemyState
{
    [SerializeField] private float _radius = 4f;
    [SerializeField] private float _waitTime = 10f;
    private NavMeshAgent _agent;
    private Vector3 _patrolCenter;
    private EnemyHealth _enemyHealth;

    [SerializeField] private Vector3 _nextPosition;
    private float _moveTimer;

    public override void Init(NavMeshAgent agent, Vector3 center, EnemyHealth enemyHealth)
    {
        isFinished = false;
        _moveTimer = Time.time + _waitTime;
        _agent = agent;

        if (!_agent.enabled)
            return;

        _patrolCenter = center;
        _nextPosition = FindPosition();
        _agent.SetDestination(_nextPosition);
        _enemyHealth = enemyHealth;
        _enemyHealth.onTakeDamage += GoToShotPoint;
    }

    private void OnDisable()
    {
        if (_enemyHealth)
        {
            if (!_enemyHealth.transform.gameObject.scene.isLoaded)
                return;
            _enemyHealth.onTakeDamage -= GoToShotPoint;
        }
    }

    public override void Run()
    {
        if (isFinished || !_agent.enabled)
            return;

        if (Vector3.Distance(_agent.transform.position, _nextPosition) < 0.1f && _moveTimer < Time.time || _moveTimer < Time.time)
        {
            isFinished = true;
        }
    }

    private Vector3 FindPosition()
    {
        float x = _patrolCenter.x + Random.Range(-_radius, _radius);
        float z = _patrolCenter.z + Random.Range(-_radius, _radius);
        Vector3 pos = new Vector3(x, 0, z);
        return pos;
    }

    private void GoToShotPoint()
    {
        if(_agent.enabled)
            _agent.SetDestination(_enemyHealth.lastShotPoint);
    }
}
