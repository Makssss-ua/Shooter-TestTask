using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public void StartReload(float reloadTime)
    {
        StartCoroutine(Reload(reloadTime));
    }

    public IEnumerator Reload(float reloadTime)
    {
        _slider.gameObject.SetActive(true);
        _slider.value = 0f;
        _slider.maxValue = reloadTime;
        float currTime = 0f;
        while(currTime <= reloadTime)
        {
            yield return new WaitForEndOfFrame();
            currTime += Time.deltaTime;
            _slider.value = currTime;
        }
        _slider.gameObject.SetActive(false);
        yield break;
    }

    public void StopReload()
    {
        _slider.gameObject.SetActive(false);
    }
}
