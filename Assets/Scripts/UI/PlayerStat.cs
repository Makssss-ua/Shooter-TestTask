using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerKills;
    [SerializeField] private TMP_Text _playerDeath;

    private void Start()
    {
        _playerKills.text = PlayerPrefs.GetInt("Player Kills").ToString();
        _playerDeath.text = PlayerPrefs.GetInt("Player Death").ToString();
    }
}
