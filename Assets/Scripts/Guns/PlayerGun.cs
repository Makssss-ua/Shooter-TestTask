using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerGun : MonoBehaviour
{
    public bool isShooting = false;
    public bool isReloading = false;
    public Transform rightHandGrip;
    public Transform leftHandGrip;
    public ItemInventory itemInventory;
    public Action updateAmmo;
    public Action onReload;

    //fire rate in second
    [SerializeField] private int _fireRate = 1;
    //max bullets in magazin
    public int mbim;
    public float reloadTime = 3f;
    [SerializeField] private int _gunDamage = 5;
    //bullets in magazin
    public int bim;
    [SerializeField] private ParticleSystem[] _muzzleFlash;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private Transform _raycastOrigin;
    [SerializeField] private TrailRenderer _bulletTrail;
    [SerializeField] private ItemInventory _startItemInventory;

    private Ray _ray;
    private RaycastHit _hit;
    private float _shootingTime = 0;

    private Camera _camera;

    private void OnEnable()
    {
        if (!itemInventory)
        {
            itemInventory = Instantiate(_startItemInventory);
        }
    }

    private void Start()
    {
        _camera = Camera.main;
        bim = itemInventory.bulletCount;

        updateAmmo?.Invoke();
        if (bim <= 0)
            Reload();
    }

    public void StartShooting()
    {
        isShooting = true;
    }

    public void UpdateShooting()
    {
        float fireInterval = 1.0f / _fireRate;

        if (isReloading)
            return;

        while (_shootingTime <= Time.time)
        {
            if (bim > 0)
            {
                FireBullet();
                bim--;
                updateAmmo?.Invoke();
                _shootingTime = Time.time + fireInterval;
            }
            else
            {
                _shootingTime = Time.time + reloadTime;
                Reload();
            }
        }
    }

    public void StopShooting()
    {
        if (bim <= 0)
            Reload();
        isShooting = false;
    }

    public void Reload()
    {
        isReloading = true;
        onReload?.Invoke();
        Invoke("Reloading", reloadTime);
    }

    private void Reloading()
    {
        isReloading = false;
        bim = mbim;
        updateAmmo?.Invoke();
    }

    private void FireBullet()
    {
        if (bim > 0)
        {
            foreach (var particle in _muzzleFlash)
            {
                particle.Emit(1);
            }
            _ray.origin = _raycastOrigin.position;

            Ray destinationRay = new Ray();
            destinationRay.origin = _camera.transform.position;
            destinationRay.direction = _camera.transform.forward;
            Physics.Raycast(destinationRay, out RaycastHit destinationHit);

            var tracer = Instantiate(_bulletTrail, _ray.origin, Quaternion.identity);
            tracer.AddPosition(_ray.origin);

            _ray.direction = destinationHit.point - _raycastOrigin.position;
            if (Physics.Raycast(_ray, out _hit))
            {
                if (_hit.point != Vector3.zero)
                {
                    EnemyHealth health = _hit.transform.GetComponent<EnemyHealth>();
                    if (health)
                    {
                        health.ChangeHealth(-_gunDamage, transform.position);
                    }
                    if (_hit.transform.gameObject.layer == LayerMask.NameToLayer("Map"))
                    {
                        _hitEffect.transform.position = _hit.point;
                        _hitEffect.transform.forward = _hit.normal;
                        _hitEffect.Emit(1);
                    }
                    tracer.transform.position = _hit.point;
                }
                else
                {
                    tracer.transform.position = _raycastOrigin.forward * 10f;
                }
            }
        }
    }
}
