using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject _winScreenObj;
    [SerializeField] private PlayerDeactivator _playerDeactivator;
    [SerializeField] private PlayerHealth _playerHealth;

    private void OnEnable()
    {
        SingReferences.enemyPool.onUpdateCount += CheckEnemyCount;
    }

    private void OnDisable()
    {
        SingReferences.enemyPool.onUpdateCount -= CheckEnemyCount;
    }

    private void CheckEnemyCount()
    {
        if(SingReferences.enemyPool.enemyCount <= 0 && _playerHealth.GetHealth() != 0)
        {
            _winScreenObj.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            _playerDeactivator.DeactivatePlayer();
        }
    }
}
