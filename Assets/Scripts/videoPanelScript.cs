using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class videoPanelScript : MonoBehaviour
{
    public MainMenuMusic coolIntro;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        gameObject.SetActive(true);
        gameObject.GetComponent<VideoPlayer>().loopPointReached += destroyPanel;

        AudioListener.volume = PlayerPrefs.GetFloat("Volume", 1);
        Debug.Log(PlayerPrefs.GetFloat("Volume", 1));
    }

    void destroyPanel(VideoPlayer vd)
    {
        coolIntro.PlayMusic();
        gameObject.SetActive(false);
    }
}
