using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidScript : MonoBehaviour
{
    //Speed at which asteroids descend down screen
    public int speed = 5;

    //Smallest possible size of an asteroid (as scaled from default)
    public float smallestSize = 0.11f;
    //Largest possible size of an asteroid (as scaled from default)
    public float largestSize = 0.2f;

    //Reference to asteroidParticles prefab, to be played on destruction
    public GameObject asteroidParticles;
    //Reference to congrats prefab, played 1/5 times on destruction
    public GameObject congrats;
    
    //Reference to GameControllerScript, holding all functionality of SpaceShooter scene
    private GameControllerScript gameController;

    // Start is called before the first frame update
    void Start()
    {
        //Get reference to GameControllerScript from GameController object
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameControllerScript>();

        //Retrieves reference to rigid body component of asteroid
        Rigidbody2D asteroidRB = GetComponent<Rigidbody2D>();

        //Causes the asteroid to decrease with random x and constant y speed, as well as with a random spin
        asteroidRB.velocity = new Vector2(Random.Range(-1.5f, 1.5f), -1 * speed);
        asteroidRB.angularVelocity = Random.Range(-200, 200);

        //Stores random potential size of asteroid, from smallest size to largest size
        float randomSize = Random.Range(smallestSize, largestSize);

        //Retrieves reference to transform of asteroid and sets its scale equal to that of randomsize
        Vector3 randomAsteroidSize = new Vector3(randomSize, randomSize, 0f);
        gameObject.transform.localScale = randomAsteroidSize;
    }

    public void DestroyAsteroid()
    {
        //Displays particles upon destruction
        Instantiate(asteroidParticles, transform.position, Quaternion.identity);

        //Randomly displays congrats message
        if (Random.Range(0, 5) == 1)
        {
            Instantiate(congrats, transform.position, Quaternion.identity);
        }
    
        //Deletes asteroid GameObject
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void OnBecameInvisible() 
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}