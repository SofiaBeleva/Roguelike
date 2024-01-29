using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    void Awake()
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            // There's no stored table, initialize
            Debug.Log("Initializing table with default values...");
            AddHighscoreEntry(1000000);
            AddHighscoreEntry(897621);
            AddHighscoreEntry(872931);
            AddHighscoreEntry(785123);
            AddHighscoreEntry(542024);
            AddHighscoreEntry(68245);
            // Reload
            jsonString = PlayerPrefs.GetString("highscoreTable");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        }

        // Sort entry list by Score
        for (int i = 0; i < highscores.highscoresEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoresEntryList.Count; j++)
            {
                if (highscores.highscoresEntryList[j].score > highscores.highscoresEntryList[i].score)
                {
                    // Swap
                    HighscoreEntry tmp = highscores.highscoresEntryList[i];
                    highscores.highscoresEntryList[i] = highscores.highscoresEntryList[j];
                    highscores.highscoresEntryList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoresEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }
    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryReñtTransform = entryTransform.GetComponent<RectTransform>();
        entryReñtTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("posText").GetComponent<TMP_Text>().text = rankString;

        int score = highscoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<TMP_Text>().text = score.ToString();

        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);
        if (rank == 1)
        {
            entryTransform.Find("posText").GetComponent<TMP_Text>().color = Color.green;
            entryTransform.Find("scoreText").GetComponent<TMP_Text>().color = Color.green;
        }
        transformList.Add(entryTransform);
    }

    private void AddHighscoreEntry(int score)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score};

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        highscores.highscoresEntryList.Add(highscoreEntry);

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.GetString("highscoreTable", json);
        PlayerPrefs.Save();
    }
    private class Highscores
    {
        public List<HighscoreEntry> highscoresEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
    } 
}
