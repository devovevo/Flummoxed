using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    AudioSource playAudioSource;
    public Slider audioSlider;

    public void Start()
    {
        audioSlider.value = PlayerPrefs.GetFloat("Volume", 1);
    }

    public void ChangeVolume()
    {
        AudioListener.volume = audioSlider.value;

        PlayerPrefs.SetFloat("Volume", audioSlider.value);
        PlayerPrefs.Save();
    }

    public void PlaySingleplayer() 
    {
        PlayerPrefs.SetString("GAME_MODE", "Singleplayer");

        playAudioSource = GetComponent<AudioSource>();
        playAudioSource.Play();

        Invoke("LoadSingleStory", 2f);
    }

    public void PlayMultiplayer()
    {
        PlayerPrefs.SetString("GAME_MODE", "Multiplayer");

        playAudioSource = GetComponent<AudioSource>();
        playAudioSource.Play();

        Invoke("LoadMultiStory", 2f);
    }

    public void PlayEndless()
    {
        PlayerPrefs.SetString("GAME_MODE", "Endless");

        playAudioSource = GetComponent<AudioSource>();
        playAudioSource.Play();

        Invoke("LoadEndlessStory", 2f);
    }

    void LoadSingleStory()
    {
        SceneManager.LoadSceneAsync("StoryScene", LoadSceneMode.Single);
    }

    void LoadEndlessStory()
    {
        SceneManager.LoadSceneAsync("StorySceneEndless", LoadSceneMode.Single);
    }

    void LoadMultiStory()
    {
        SceneManager.LoadSceneAsync("StorySceneMultiplayer", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
