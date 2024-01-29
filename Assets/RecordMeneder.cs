using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecordMeneder : MonoBehaviour
{
    [SerializeField] TMP_Text Record;
    [SerializeField] TMP_Text NowRecord;

    public static float record;
    int highrecord;
    // Start is called before the first frame update
    void Start()
    {
        record = 0;
    }

    // Update is called once per frame
    void Update()
    {
        highrecord = (int)record;
        NowRecord.text = highrecord.ToString();
        if (PlayerPrefs.GetInt("record") <= highrecord)
        {
            PlayerPrefs.SetInt("record", highrecord);
            GetComponent<HighscoreTable>().AddHighscoreEntry(highrecord);
            Debug.Log("Current High Record: " + highrecord); 
            Debug.Log("Stored High Record: " + PlayerPrefs.GetInt("record"));
        }
        Record.text = "RECORD " + PlayerPrefs.GetInt("record").ToString();

    }
}