using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyList = new List<GameObject>();
    public int enemyCount { get; private set; } = 0;
    public Action onUpdateCount;

    private void Start()
    {
        foreach(var enemy in _enemyList)
        {
            if (enemy.activeSelf)
            {
                EnemyPoolObject enemyPoolObject = enemy.AddComponent<EnemyPoolObject>();
                enemyPoolObject.enemyPool = this;
                enemyCount++;
                onUpdateCount?.Invoke();
            }
        }
    }

    public void RemoveEnemyFromPool(GameObject enemy)
    {
        _enemyList.Remove(enemy);
        enemyCount--;
        onUpdateCount?.Invoke();
    }
}
