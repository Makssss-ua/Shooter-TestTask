using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Transform _rightHandGrip;
    [SerializeField] private Transform _leftHandGrip;
    [SerializeField] private Transform _weaponPivot;
    [SerializeField] private CinemachineFreeLook _playerAimCamera;
    [SerializeField] private ReloadBar _reloadBar;

    public PlayerGun playerGun { get; private set; }
    public Action updateAmmo;

    public PlayerGun GetPivotWeapon()
    {
        PlayerGun existGun = _weaponPivot.GetComponentInChildren<PlayerGun>();
        if (existGun)
        {
            return existGun;
        }
        return null;
    }

    private void LateUpdate()
    {
        if (playerGun != null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                playerGun.StartShooting();
            }
            if (playerGun.isShooting)
            {
                playerGun.UpdateShooting();
            }
            if (Input.GetButtonUp("Fire1"))
            {
                playerGun.StopShooting();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                playerGun.Reload();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                _playerAimCamera.m_Lens.FieldOfView = 20f;
            }
            if (Input.GetButtonUp("Fire2"))
            {
                _playerAimCamera.m_Lens.FieldOfView = 50f;
            }
        }
    }

    public void GetWeapon(PlayerGun newWeapon)
    {
        if (playerGun)
        {
            playerGun.onReload -= StartReloadBar;
            Destroy(playerGun.gameObject);
        }
        playerGun = newWeapon;
        playerGun.transform.parent = _weaponPivot;
        playerGun.transform.localPosition = Vector3.zero;
        playerGun.transform.localRotation = Quaternion.identity;
        playerGun.updateAmmo = updateAmmo;
        _reloadBar.StopReload();
        playerGun.onReload += StartReloadBar;

        _leftHandGrip.position = playerGun.leftHandGrip.position;
        _leftHandGrip.localRotation = playerGun.leftHandGrip.localRotation;
        _rightHandGrip.position = playerGun.rightHandGrip.position;
        _rightHandGrip.localRotation = playerGun.rightHandGrip.localRotation;
    }

    public void Die()
    {
        Rigidbody rb = playerGun.transform.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(-rb.transform.forward);
        }
    }

    private void StartReloadBar()
    {
        _reloadBar.StartCoroutine(_reloadBar.Reload(playerGun.reloadTime));
    }
}
