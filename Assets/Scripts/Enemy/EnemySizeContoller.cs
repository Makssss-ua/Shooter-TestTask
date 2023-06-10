using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySizeContoller : MonoBehaviour
{
    [SerializeField] private Vector2 _sizeRange = Vector2.one;
    [SerializeField] private Health _health;
    public float size;

    private void OnEnable()
    {
        size = Random.Range(_sizeRange.x, _sizeRange.y);
        _health.SetMaxHealth(size * _health.GetMaxHealth());
        _health.ChangeHealth(_health.GetMaxHealth());

        transform.localScale *= size;
    }

    public IEnumerator Die(Transform enemyTransform)
    {
        Vector3 scale = enemyTransform.localScale;
        float scaleSpeed = 0.2f;
        while (scale != Vector3.zero)
        {
            yield return new WaitForFixedUpdate();
            if (scale.magnitude > 1f)
            {
                scaleSpeed = 0.2f;
            }
            else
            {
                scaleSpeed = 10f;
            }

            scale.x = Mathf.Lerp(scale.x, 0f, Time.deltaTime * scaleSpeed);
            scale.y = Mathf.Lerp(scale.y, 0f, Time.deltaTime * scaleSpeed);
            scale.z = Mathf.Lerp(scale.z, 0f, Time.deltaTime * scaleSpeed);

            enemyTransform.localScale = scale;
        }
        Destroy(enemyTransform.gameObject);
        yield break;
    }
}
