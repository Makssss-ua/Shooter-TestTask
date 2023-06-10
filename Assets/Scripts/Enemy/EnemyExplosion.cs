using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{
    [SerializeField] private float _damage = 25f;
    [SerializeField] private float _exploseRadius = 1f;
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private GameObject _enemyObj;

    private List<Health> _entityInRange = new List<Health>();

    private void Start()
    {
        _sphereCollider.radius = _exploseRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<PlayerHealth>();
        if (health)
        {
            _entityInRange.Add(health);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Health health = other.GetComponent<PlayerHealth>();
        if (health)
        {
            _entityInRange.Remove(health);
        }
    }

    public void BlowUp()
    {
        foreach(var entity in _entityInRange)
        {
            entity.ChangeHealth(-_damage);
        }
        Destroy(Instantiate(_explosionEffect.gameObject, transform.position, Quaternion.identity), _explosionEffect.main.duration);
        Destroy(_enemyObj);
    }
}
