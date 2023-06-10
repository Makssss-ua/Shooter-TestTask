using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveContent : MonoBehaviour
{
    [SerializeField] private GameObject[] _healthPoints;
    public int healthPointsCount { get; private set; }
    public Image background;

    private void Start()
    {
        if(!background)
            background = GetComponent<Image>();
        healthPointsCount = _healthPoints.Length;
    }

    public void SetActivePoints(int count)
    {
        for(int i = 0; i < _healthPoints.Length; i++)
        {
            _healthPoints[i].SetActive(true);
        }
        for(int i = count; i < _healthPoints.Length; i++)
        {
            _healthPoints[i].SetActive(false);
        }
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = Camera.main.WorldToScreenPoint(pos);
    }
}
