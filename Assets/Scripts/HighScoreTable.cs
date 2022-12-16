using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreTable : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;

    private List<HighScoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;

    public GameObject congrats;

    float templateHeight = 65f;

    Vector3 playerLocation;

    int currentPlayerPosition = 0; 

    // Start is called before the first frame update
    void Start()
    {
        int currentScore = PlayerPrefs.GetInt("currentPlayerScore", 0);
        string currentName = PlayerPrefs.GetString("currentPlayerName", "NA");

        AddHighScoreEntry(currentScore, currentName);

        string highScoreJSON = PlayerPrefs.GetString("highscoreTable");
        Debug.Log(highScoreJSON);
        Highscores highScores = JsonUtility.FromJson<Highscores>(highScoreJSON);

        highscoreEntryList = highScores.highscoreList;

        highscoreEntryTransformList = new List<Transform>();

        if (currentPlayerPosition + 1 < 7)
        {
            for (int i = 0; i < highscoreEntryList.Count && i < 7; i++) {
                CreateHighScoreEntryTransform(highscoreEntryList[i], entryContainer, highscoreEntryTransformList, false);
            }   
        }
        else
        {
            for (int i = 0; i < highscoreEntryList.Count && i < 6; i++) {
                CreateHighScoreEntryTransform(highscoreEntryList[i], entryContainer, highscoreEntryTransformList, false);
            }

            CreateHighScoreEntryTransform(highscoreEntryList[currentPlayerPosition], entryContainer, highscoreEntryTransformList, true);
        }

        //PlayerPrefs.SetString("highscoreTable", "");
    }

    void SpawnCongrats()
    {
        Vector3 randLoc = playerLocation;
        randLoc.x = Random.Range(playerLocation.x, playerLocation.x + 10);

        Instantiate(congrats, randLoc, Quaternion.identity).transform.localScale = new Vector3(2f, 2f);
    }

    private void CreateHighScoreEntryTransform(HighScoreEntry highscoreEntry, Transform container, List<Transform> transformList, bool isPlayer)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);

        Debug.Log(entryRectTransform.anchoredPosition);

        TMP_Text rankTextObject = entryTransform.Find("rank").GetComponent<TMP_Text>();
        TMP_Text scoreTextObject = entryTransform.Find("score").GetComponent<TMP_Text>();
        TMP_Text nameTextObject = entryTransform.Find("name").GetComponent<TMP_Text>();
        ParticleSystem playerparticles = entryTransform.Find("playerstarparticles").GetComponent<ParticleSystem>();

        int actualRank = 0;

        if (isPlayer)
        {
            actualRank = currentPlayerPosition + 1;
        }
        else
        {
            actualRank = transformList.Count + 1;
        }

        string rankString;

        switch (actualRank) {
            default:
                rankString = actualRank + "TH";
                break;
            
            case 1:
                rankString = "1ST";

                rankTextObject.color = Color.green;
                scoreTextObject.color = Color.green;
                nameTextObject.color = Color.green;

                break;

            case 2:
                rankString = "2ND";

                rankTextObject.color = Color.magenta;
                scoreTextObject.color = Color.magenta;
                nameTextObject.color = Color.magenta;

                break;
            
            case 3:
                rankString = "3RD";

                rankTextObject.color = Color.yellow;
                scoreTextObject.color = Color.yellow;
                nameTextObject.color = Color.yellow;

                break;
        }

        if (actualRank % 2 != 0)
        {
            rankTextObject.alpha = 0.8f;
            scoreTextObject.alpha = 0.8f;
            nameTextObject.alpha = 0.8f;
        }

        if(actualRank == (currentPlayerPosition + 1))
        {
            playerLocation = playerparticles.transform.position;
            InvokeRepeating("SpawnCongrats", 0f, 1.5f);
        }
        
        if (actualRank != (currentPlayerPosition + 1))
        {
            playerparticles.Stop();
        }

        rankTextObject.text = rankString;
        
        int score = highscoreEntry.score;
        scoreTextObject.text = score.ToString();

        string name = highscoreEntry.name;
        nameTextObject.text = name;

        transformList.Add(entryTransform);
    }

    private void AddHighScoreEntry(int newScore, string newName) {
        HighScoreEntry newHighScoreEntry = new HighScoreEntry{ score = newScore, name = newName };

        string oldHighscoreJSON = PlayerPrefs.GetString("highscoreTable");
        Highscores oldHighscores = JsonUtility.FromJson<Highscores>(oldHighscoreJSON);

        if (oldHighscores != null)
        {
            currentPlayerPosition = oldHighscores.highscoreList.Count;

            for (int i = 0; i < oldHighscores.highscoreList.Count; i++)
            {
                if(newScore > oldHighscores.highscoreList[i].score)
                {  
                    currentPlayerPosition = i;
                    i = oldHighscores.highscoreList.Count + 1;
                }
            }
        }
        else
        {
            oldHighscores = new Highscores{ highscoreList = new List<HighScoreEntry>() };
        }

        oldHighscores.highscoreList.Insert(currentPlayerPosition, newHighScoreEntry);

        string modifiedHighscoreJSON = JsonUtility.ToJson(oldHighscores);
        PlayerPrefs.SetString("highscoreTable", modifiedHighscoreJSON);
        PlayerPrefs.Save();
    }

    private class Highscores {
        public List<HighScoreEntry> highscoreList;
    }

    [System.Serializable]
    private class HighScoreEntry {
        public int score;
        public string name;
    }
}
