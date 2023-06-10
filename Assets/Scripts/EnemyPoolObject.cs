using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolObject : MonoBehaviour
{
    public EnemyPool enemyPool;

    private void OnDestroy()
    {
        enemyPool.RemoveEnemyFromPool(gameObject);
    }
}
