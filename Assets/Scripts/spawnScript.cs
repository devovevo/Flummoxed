using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnScript : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject ufo;
    public GameObject brain;
    public GameObject planet;

    public int pauseBetweenLevelsTime;

    public float asteroidSpawnTime = 0.35f;
    public float ufoSpawnTime = 4.0f;
    public float minPlanetSpawnTime;
    public float maxPlanetSpawnTime;

    float x1;
    float x2;

    private GameControllerScript gameController;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameControllerScript>();

        Renderer spawnR = GetComponent<Renderer>();

        x1 = transform.position.x - spawnR.bounds.size.x / 2;
        x2 = transform.position.x + spawnR.bounds.size.x / 2;

        Vector2 initialPoint;

        for (int i = 0; i <= 5; i++)
        {
            initialPoint = new Vector2(Random.Range(x1 - 3, x2 + 3), Random.Range(-7, 7.5f));
            Instantiate(planet, initialPoint, Quaternion.identity);
        }

        SpawnPlanet();

        if (gameController.gamemode != "Endless")
        {
            Level1();
        }
        else
        {
            RandomSpawn();
        }
    }

    public void RandomSpawn()
    {
        if (gameController.level > 0)
        {
            if (gameController.level % 6 == 0)
            {
                Invoke("spawnBrain", 5f);
            }

            int numBrains = GameObject.FindGameObjectsWithTag("Brain").Length;

            float randAsteroidSpawnTime = asteroidSpawnTime / Mathf.Log(gameController.level * 0.4f + 1) * (numBrains + 1);
            float randUfoSpawnTime = ufoSpawnTime / Mathf.Log(gameController.level * 0.4f + 1) * (numBrains + 1);

            InvokeRepeating("spawnAsteroid", 0, randAsteroidSpawnTime);

            if (gameController.level > 2)
            {
                InvokeRepeating("spawnUFO", 0, randUfoSpawnTime);
            }
        }
    }

    void SpawnPlanet()
    {
        if (Time.timeSinceLevelLoad < (gameController.timeforWin - 30))
        {
            Vector2 spawnPoint = new Vector2(Random.Range(x1 - 3, x2 + 3), transform.position.y + 3);

            Instantiate(planet, spawnPoint, Quaternion.identity);

            Invoke("SpawnPlanet", Random.Range(minPlanetSpawnTime, maxPlanetSpawnTime));
        }
    }

    void spawnAsteroid()
    {
        Vector2 spawnPoint = new Vector2(Random.Range(x1, x2), transform.position.y);

        Instantiate(asteroid, spawnPoint, Quaternion.identity);
    }

    void spawnUFO()
    {
        Vector2 spawnPoint = new Vector2(Random.Range(x1, x2), transform.position.y);

        Instantiate(ufo, spawnPoint, Quaternion.identity);
    }

    void spawnBrain()
    {
        Instantiate(brain, transform.position, Quaternion.identity);
    }

    public void Level1()
    {
        InvokeRepeating("spawnAsteroid", 0, asteroidSpawnTime);
    }

    public void Level2()
    {
        InvokeRepeating("spawnUFO", 0, ufoSpawnTime);
    }

    public void Level3()
    {

        InvokeRepeating("spawnUFO", 0, ufoSpawnTime);
        InvokeRepeating("spawnAsteroid", 0, asteroidSpawnTime);
    }

    public void Level4()
    {
        Invoke("spawnBrain", 2f);
    }

    public void Level6()
    {
        Invoke("spawnBrain", 2f);
        Invoke("spawnBrain", 6f);
        InvokeRepeating("spawnUFO", 0, ufoSpawnTime);
    }

    public void Level5()
    {
        Invoke("spawnBrain", 2f);
        InvokeRepeating("spawnAsteroid", 0, asteroidSpawnTime);
    }

    public void ResetSpawn()
    {
        CancelInvoke();
        SpawnPlanet();
    }
}