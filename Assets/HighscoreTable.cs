﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HighscoreTable : MonoBehaviour
{
    public Text leaderboardText;

    private const int MaxEntries = 5;
    private List<int> highScores;

    private void Start()
    {
        LoadHighScores();
        UpdateLeaderboardText();
    }

    public void UpdateLeaderboard(int score)
    {
        // Добавление нового результата и сортировка
        highScores.Add(score);
        highScores = highScores.OrderByDescending(x => x).Take(MaxEntries).ToList();

        SaveHighScores();
        UpdateLeaderboardText();
    }

    public void LoadHighScores()
    {
        // Загрузка рекордов из PlayerPrefs, если они есть
        highScores = new List<int>();

        for (int i = 0; i < MaxEntries; i++)
        {
            int score = PlayerPrefs.GetInt($"HighScore{i}", 0);
            if (score > 0)
            {
                highScores.Add(score);
            }
        }
    }

    private void SaveHighScores()
    {
        // Сохранение рекордов в PlayerPrefs
        for (int i = 0; i < highScores.Count; i++)
        {
            PlayerPrefs.SetInt($"HighScore{i}", highScores[i]);
        }

        PlayerPrefs.Save();
    }

    private void UpdateLeaderboardText()
    {
        // Обновление текста таблицы рекордов
        string text = "";

        for (int i = 0; i < highScores.Count; i++)
        {
            text += $"{i + 1}                                               {highScores[i]}\n";
        }

        leaderboardText.text = text;
        Debug.Log("Updated leaderboard text: " + text);
    }
}