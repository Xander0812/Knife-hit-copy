using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Этот скрипт хранит в себе информацию о колличестве валюты игрока и не разрушается при перезагрузке. Весь прогресс игрока сохраняется.

    public static GameManager Instance { get; private set; }

    private const string TOTAL_ORANGES = "TotalOranges";
    private const string TOTAL_SCORE = "TotalScore";
    private const string MAX_SCORE = "MaxScore";

    public int TotatlOranges
    {
        get => PlayerPrefs.GetInt(key: TOTAL_ORANGES, defaultValue: 0);
        set => PlayerPrefs.SetInt(TOTAL_ORANGES, value);
    }

    public int TotatlScore
    {
        get => PlayerPrefs.GetInt(key: TOTAL_SCORE, defaultValue: 0);
        set => PlayerPrefs.SetInt(TOTAL_SCORE, value);
    }

    public int MaxScore
    {
        get => PlayerPrefs.GetInt(key: MAX_SCORE, defaultValue: 0);
        set => PlayerPrefs.SetInt(MAX_SCORE, value);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
