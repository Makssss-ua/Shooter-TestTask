using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDetector : MonoBehaviour
{
    [SerializeField] private float _viewRange = 90f;
    [SerializeField] private Transform _enemyEye;
    [SerializeField] private Transform _enemyTransform;
    [SerializeField] private EnemySizeContoller _sizeController;
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private float _aggreZone = 2f;

    public float viewDistance = 10f;
    public PlayerHealth player { get; private set; }
    public bool seePlayer;

    private void Start()
    {
        viewDistance *= _sizeController.size;
        _sphereCollider.radius *= _sizeController.size;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player)
        {
            this.player = player;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player)
        {
            this.player = null;
        }
    }

    private void FixedUpdate()
    {
        DebugDrawLine();
        if (player)
            seePlayer = InViewRange(player.transform, viewDistance);
        else
            seePlayer = false;
    }

    public bool InViewRange(Transform target, float distance)
    {
        Vector3 targetpos = target.transform.position + new Vector3(0, 0.5f, 0);
        float angle = Vector3.Angle(_enemyEye.forward, targetpos - _enemyEye.position);
        float distanceToTarget = Vector3.Distance(_enemyEye.position, targetpos);
        if(distanceToTarget < _aggreZone * _sizeController.size)
        {
            RaycastHit hit;
            if (Physics.Raycast(_enemyEye.position, targetpos - _enemyEye.position, out hit, distance))
            {
                Debug.DrawLine(_enemyEye.position, targetpos, Color.yellow);
                if (hit.transform == target.transform)
                {
                    return true;
                }
            }
        }
        else
        if (angle <= _viewRange / 2f && distanceToTarget <= distance)
        {
            RaycastHit hit;
            if (Physics.Raycast(_enemyEye.position, targetpos - _enemyEye.position, out hit, distance))
            {
                Debug.DrawLine(_enemyEye.position, targetpos, Color.yellow);
                if (hit.transform == target.transform)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void DebugDrawLine()
    {
        Vector3 right = _enemyEye.position + Quaternion.Euler(new Vector3(0f, _viewRange / 2f, 0f)) * (_enemyEye.forward * viewDistance);
        Vector3 left = _enemyEye.position + Quaternion.Euler(-new Vector3(0f, _viewRange / 2f, 0f)) * (_enemyEye.forward * viewDistance);
        Debug.DrawLine(_enemyEye.position, left, Color.red);
        Debug.DrawLine(_enemyEye.position, right, Color.red);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 0, 0.2f);
        Gizmos.DrawSphere(_enemyEye.position, _aggreZone * _sizeController.size);
    }
}
