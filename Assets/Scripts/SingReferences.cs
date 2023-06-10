using UnityEngine;

public class SingReferences : MonoBehaviour
{
    [SerializeField] private MasterManager _masterManager;
    [SerializeField] private EnemyPool _enemyPool;
    public static EnemyPool enemyPool;

    private void OnEnable()
    {
        enemyPool = _enemyPool;
    }
}
