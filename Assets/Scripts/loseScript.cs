using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loseScript : MonoBehaviour
{
    public AudioClip continueSound;

    AudioSource loseClickSource;

    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume", 1);
        loseClickSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    public void Continue()
    {
        loseClickSource.PlayOneShot(continueSound);

        Invoke("LoadLeaderboard", 2);
    }
    
    public void LoadLeaderboard()
    {
        SceneManager.LoadSceneAsync("LeaderboardScene", LoadSceneMode.Single);
    }
}
