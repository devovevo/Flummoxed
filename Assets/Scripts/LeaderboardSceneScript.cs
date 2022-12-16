using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardSceneScript : MonoBehaviour
{
    public AudioClip homeSound;
    public AudioClip replaySound;
    
    AudioSource leaderboardAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume", 1);
        leaderboardAudioSource = GetComponent<AudioSource>();
    }

    public void Replay()
    {
        leaderboardAudioSource.PlayOneShot(replaySound);

        Invoke("LoadShooter", 2);
    }

    void LoadShooter()
    {
        SceneManager.LoadSceneAsync("SpaceShooter", LoadSceneMode.Single);
    }

    void LoadHome()
    {
        SceneManager.LoadSceneAsync("TitleScreen", LoadSceneMode.Single);
    }

    public void Home()
    {
        leaderboardAudioSource.PlayOneShot(homeSound);
        
        Invoke("LoadHome", 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
