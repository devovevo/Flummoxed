using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class playGameButtonScript : MonoBehaviour
{
    public TMP_InputField playerName;
    AudioSource buttonAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume", 1);
        buttonAudioSource = GetComponent<AudioSource>();
    }

    public void PlayGame()
    {
        if(playerName.text.Length > 2)
        {
            PlayerPrefs.SetString("currentPlayerName", playerName.text);
            PlayerPrefs.Save();

            buttonAudioSource.Play();

            Invoke("LoadShooter", 2f);
        }
    }

    void LoadShooter()
    {   
        SceneManager.LoadSceneAsync("SpaceShooter", LoadSceneMode.Single);
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
