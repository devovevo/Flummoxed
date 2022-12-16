using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winScript : MonoBehaviour
{
    AudioSource leaderboardPressAudio;

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume", 1);
        leaderboardPressAudio = GetComponent<AudioSource>();
    }
    public void GoLeaderboard()
    {
        leaderboardPressAudio.Play();
        
        Invoke("LoadLeaderboard", 2);
    }

    void LoadLeaderboard()
    {
        SceneManager.LoadSceneAsync("LeaderboardScene", LoadSceneMode.Single);
    }
}
