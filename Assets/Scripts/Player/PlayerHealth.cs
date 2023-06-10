using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Cinemachine;

public class PlayerHealth : Health
{
    [SerializeField] private Animator _anim;
    [SerializeField] private PlayerDeactivator _playerDeactivator;
    [SerializeField] private PlayerWeapon _playerWeapon;
    [SerializeField] private RigBuilder _rigBuilder;
    [SerializeField] private GameObject _loseScreen;

    protected override void OnEnable()
    {
        base.OnEnable();
        onUpdateHealth += CheckHealth;
    }

    private void OnDisable()
    {
        onUpdateHealth -= CheckHealth;
    }

    private void CheckHealth()
    {
        if(_currentHealth <= 0f)
        {
            PlayerPrefs.SetInt("Player Death", PlayerPrefs.GetInt("Player Death")+1);
            _anim.SetTrigger("death");
            _playerDeactivator.DeactivatePlayer();
            _loseScreen.SetActive(true);
            _playerWeapon.Die();
            _rigBuilder.enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
