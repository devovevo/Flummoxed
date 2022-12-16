using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public int score;
    public int level;
    public int singleplayerLives;
    public int multiplayerLives;
    public int currentLives;
    public bool paused;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI levelText;

    public Vector3 playerShipInitialPosition;

    public GameObject spawner;
    public spawnScript spawnerScriptRef;

    public GameObject playerShip;
    public GameObject leftShip;
    public GameObject rightShip;
    public GameObject scorePopup;

    public bool respawnedSingleRecently;
    public bool respawnedRightRecently;
    public bool respawnedLeftRecently;

    public int timefor2;
    public int timefor3;
    public int timefor4;
    public int timefor5;
    public int timefor6;
    public int timeforWin;

    public AudioClip level1Soundtrack;
    public AudioClip level2Soundtrack;
    public AudioClip level3Soundtrack;
    public AudioClip level4Soundtrack;
    public AudioClip level5Soundtrack;
    public AudioClip level6Soundtrack;

    public AudioClip endlessSoundtrack;

    public AudioClip levelUpSound;
    public AudioClip loseSound;
    public AudioClip homeSound;
    public AudioClip pauseSound;
    public AudioClip playSound;
    public AudioClip winSound;

    AudioSource gameAudio;

    public string gamemode = "";
    public Slider audioSlider;

    public void changeVolume()
    {
        AudioListener.volume = audioSlider.value;

        PlayerPrefs.SetFloat("Volume", audioSlider.value);
        PlayerPrefs.Save();
    }

    // Start is called before the first frame update
    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1);

        audioSlider.value = savedVolume;
        AudioListener.volume = savedVolume;

        Time.timeScale = 1;
        paused = false;

        gameAudio = GetComponent<AudioSource>();

        spawner = GameObject.FindGameObjectWithTag("Spawner");
        spawnerScriptRef = spawner.GetComponent<spawnScript>();

        respawnedSingleRecently = false;
        respawnedLeftRecently = false;
        respawnedRightRecently = false;

        gamemode = PlayerPrefs.GetString("GAME_MODE");

        Reset();
    }

    void Reset()
    {
        setScore(score);
        setLevel(level);

        if (gamemode == "Multiplayer")
        {
            SpawnLeftShip();
            SpawnRightShip();

            setLives(multiplayerLives);
            currentLives = multiplayerLives;
        }
        else
        {
            SpawnSinglePlayerShip();
            currentLives = singleplayerLives;
        }

        setLives(currentLives);
    }

    void SpawnSinglePlayerShip()
    {
        Instantiate(playerShip, playerShipInitialPosition, Quaternion.identity);
    }

    void SpawnLeftShip()
    {
        Instantiate(leftShip, playerShipInitialPosition, Quaternion.identity);
    }

    void SpawnRightShip()
    {
        Instantiate(rightShip, playerShipInitialPosition, Quaternion.identity);
    }

    void setScore(int newScore)
    {
        score = newScore;
        scoreText.text = "Score: " + score;
    }

    void setLives(int newLives)
    {
        currentLives = newLives;
        livesText.text = "Lives: " + currentLives;
    }

    void setLevel(int newLevel)
    {
        level = newLevel;
        levelText.text = "Level: " + newLevel;
    }

    public void incrementScore(int incrementAmount)
    {
        setScore(score + incrementAmount);

        Vector3 scoreLoc = scoreText.transform.position;
        scoreLoc.x += 2.3f;
        scoreLoc.y += 0.5f;

        Instantiate(scorePopup, scoreLoc, Quaternion.identity).GetComponentInChildren<TextMeshProUGUI>().text = ("+" + incrementAmount);
    }

    public void singleShipDied()
    {
        if (respawnedSingleRecently == false)
        {
            currentLives--;

            if (currentLives > 0)
            {
                setLives(currentLives);
                SpawnSinglePlayerShip();

                respawnedSingleRecently = true;
            }
            else
            {
                setLives(0);
                StartLose();
            }
        }
    }

    public void leftShipDied()
    {
        if (respawnedLeftRecently == false)
        {
            currentLives--;

            if (currentLives > 0)
            {
                setLives(currentLives);
                SpawnLeftShip();

                respawnedLeftRecently = true;
            }
            else
            {
                setLives(0);
                StartLose();
            }
        }
    }

    public void rightShipDied()
    {
        if (respawnedRightRecently == false)
        {
            currentLives--;

            if (currentLives > 0)
            {
                setLives(currentLives);
                SpawnRightShip();

                respawnedRightRecently = true;
            }
            else
            {
                setLives(0);
                StartLose();
            }
        }
    }

    public void StartLose()
    {
        Time.timeScale = 0.5f;
        level = 0;
        gameAudio.Stop();

        gameAudio.PlayOneShot(loseSound);

        PlayerPrefs.SetInt("currentPlayerScore", score);
        PlayerPrefs.Save();

        Invoke("Lose", 2);
    }

    public void Pause()
    {
        gameAudio.Stop();
        
        gameAudio.PlayOneShot(pauseSound);
        paused = true;

        Time.timeScale = 0.25f;

        Invoke("StopTime", 0.1f);
    }

    void StopTime()
    {
        Time.timeScale = 0;
        mute();
    }

    void mute()
    {
        AudioListener.volume = 0;
    }

    void unmute()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume", 1);
    }

    public void Resume()
    {
        unmute();
        Time.timeScale = 1;

        gameAudio.PlayOneShot(playSound);
        paused = false;
    }

    public void Home()
    {
        gameAudio.PlayOneShot(homeSound);

        LoadHome();
    }

    void LoadHome()
    {
        SceneManager.LoadSceneAsync("TitleScreen", LoadSceneMode.Single);
    }

    void Lose()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("LoseScene", LoadSceneMode.Single);
    }

    void Win()
    {
        if (currentLives > 0)
        {
            gameAudio.Stop();

            gameAudio.PlayOneShot(winSound);

            PlayerPrefs.SetInt("currentPlayerScore", score);
            PlayerPrefs.Save();
            
            Invoke("LoadWinScene", 6);
        }  
    }

    void LoadWinScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("WinScene", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (paused == false)
        {
            if (gamemode != "Endless" && level > 0 && level < 7)
            {
                if (level != 2 && Time.timeSinceLevelLoad > timefor2 && Time.timeSinceLevelLoad < timefor3)
                {
                    setLevel(2);

                    spawnerScriptRef.ResetSpawn();
                    spawnerScriptRef.Level2();

                    gameAudio.Stop();

                    gameAudio.PlayOneShot(levelUpSound);
                }
                else if (level != 3 && Time.timeSinceLevelLoad > timefor3 && Time.timeSinceLevelLoad < timefor4)
                {
                    setLevel(3);

                    spawnerScriptRef.ResetSpawn();
                    spawnerScriptRef.Level3();

                    gameAudio.Stop();

                    gameAudio.PlayOneShot(levelUpSound);
                }
                else if (level != 4 && Time.timeSinceLevelLoad > timefor4 && Time.timeSinceLevelLoad < timefor5)
                {
                    setLevel(4);

                    spawnerScriptRef.ResetSpawn();
                    spawnerScriptRef.Level4();

                    gameAudio.Stop();

                    gameAudio.PlayOneShot(levelUpSound);
                }
                else if (level != 5 && Time.timeSinceLevelLoad > timefor5 && Time.timeSinceLevelLoad < timefor6)
                {
                    setLevel(5);

                    spawnerScriptRef.ResetSpawn();
                    spawnerScriptRef.Level5();

                    gameAudio.Stop();

                    gameAudio.PlayOneShot(levelUpSound);
                }
                else if (level != 6 && Time.timeSinceLevelLoad > timefor6 && Time.timeSinceLevelLoad < timeforWin)
                {
                    setLevel(6);

                    spawnerScriptRef.ResetSpawn();
                    spawnerScriptRef.Level6();

                    gameAudio.Stop();

                    gameAudio.PlayOneShot(levelUpSound);
                }
                else if (level != 7 && Time.timeSinceLevelLoad > timeforWin)
                {
                    level = 7;

                    spawnerScriptRef.ResetSpawn();

                    gameAudio.PlayOneShot(levelUpSound);

                    Invoke("Win", 10);
                }

                if (gameAudio.isPlaying == false)
                {
                    if (level == 1)
                    {
                        gameAudio.PlayOneShot(level1Soundtrack);
                    }
                    else if (level == 2)
                    {
                        gameAudio.PlayOneShot(level2Soundtrack);
                    }
                    else if (level == 3)
                    {
                        gameAudio.PlayOneShot(level3Soundtrack);
                    }
                    else if (level == 4)
                    {
                        gameAudio.PlayOneShot(level4Soundtrack);
                    }
                    else if (level == 5)
                    {
                        gameAudio.PlayOneShot(level5Soundtrack);
                    }
                    else if (level == 6)
                    {
                        gameAudio.PlayOneShot(level6Soundtrack);
                    }
                }
            }
            else if (gamemode == "Endless")
            {
                if ((level != Mathf.FloorToInt(Time.timeSinceLevelLoad / 40) + 1) && level != 0)
                {
                    setLevel(level + 1);

                    spawnerScriptRef.ResetSpawn();
                    spawnerScriptRef.RandomSpawn();

                    gameAudio.PlayOneShot(levelUpSound);
                }

                if (gameAudio.isPlaying == false)
                {
                    gameAudio.PlayOneShot(endlessSoundtrack);
                }
            }
        }
    }
}
