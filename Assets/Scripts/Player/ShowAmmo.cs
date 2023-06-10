using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowAmmo : MonoBehaviour
{
    [SerializeField] private PlayerWeapon _playerWeapon;
    [SerializeField] private Text _currentAmmo;
    [SerializeField] private Text _maxAmmo;

    private void OnEnable()
    {
        _playerWeapon.updateAmmo += UpdateAmmo;
    }

    private void OnDisable()
    {
        _playerWeapon.updateAmmo -= UpdateAmmo;
    }

    private void UpdateAmmo()
    {
        _currentAmmo.text = _playerWeapon.playerGun.bim.ToString();
        _maxAmmo.text = _playerWeapon.playerGun.mbim.ToString();
    }
}
