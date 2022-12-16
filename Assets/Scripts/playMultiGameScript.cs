using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class playMultiGameScript : MonoBehaviour
{
    public TMP_InputField player1Name;
    public TMP_InputField player2Name;

    AudioSource buttonAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        buttonAudioSource = GetComponent<AudioSource>();
    }

    public void PlayMultiGame()
    {
        if(player1Name.text.Length > 2 && player2Name.text.Length > 2)
        {
            PlayerPrefs.SetString("currentPlayerName", player1Name.text + "+" + player2Name.text);
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
