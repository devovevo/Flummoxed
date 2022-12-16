using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    AudioSource menuMusicSource;

    // Start is called before the first frame update
    void Start()
    {
        menuMusicSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        menuMusicSource.Play();
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