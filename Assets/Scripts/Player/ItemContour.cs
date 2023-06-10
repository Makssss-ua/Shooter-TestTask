using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContour : MonoBehaviour
{
    [SerializeField] private Image _itemContourImage;
    [SerializeField] private Color _activeColor;
    private Color _startColor;

    private void Start()
    {
        _startColor = _itemContourImage.color;
    }

    public void ActiveContour()
    {
        _itemContourImage.color = _activeColor;
    }

    public void DisableContour()
    {
        _itemContourImage.color = _startColor;
    }
}
