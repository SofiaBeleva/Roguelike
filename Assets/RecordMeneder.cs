using UnityEngine;
using UnityEngine.UI;

public class RecordMeneder : MonoBehaviour
{
    [SerializeField] Text Record;
    [SerializeField] HighscoreTable leaderboard;

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
        PlayerPrefs.SetInt("record", highrecord);
        Record.text = "RECORD " + PlayerPrefs.GetInt("record").ToString();
        if (GameController.TF)
        {
                leaderboard.UpdateLeaderboard(highrecord);
        }
    }
}