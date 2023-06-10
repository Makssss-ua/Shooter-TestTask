using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Cinemachine;

public class PlayerDeactivator : MonoBehaviour
{

    [SerializeField] private PlayerRotation _playerRotation;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private PlayerWeapon _playerWeapon;
    [SerializeField] private CharacterLocomotion _characterLocomotion;
    [SerializeField] private CinemachineFreeLook _cmCamera;
    [SerializeField] private GameObject _playerUI;

    public void DeactivatePlayer()
    {
        _playerRotation.enabled = false;
        _playerMove.enabled = false;
        //
        _cmCamera.enabled = false;
        _playerWeapon.enabled = false;
        _characterLocomotion.enableInput = false;
        _playerUI.SetActive(false);
    }
}
