using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundAsteroidScript : MonoBehaviour
{
    //Reference to RigidBody component of backgroundAsteroid GameObject
    Rigidbody2D asteroidRigidBody;

    //Holds an integer, to be randomly assigned at start, which determines which sprite should be rendered for the backgroundAsteroid GameObject
    int randSpriteInt;

    //Reference to the asteroidParticles prefab, to be played on destruction for asteroid sprites
    public GameObject asteroidParticles;
    //Reference to ufoParticles prefab, to be played on destruction for satellite sprites
    public GameObject satelliteParticles;
    //Refernece to CongratsMessage prefab, to be played occasionally on destruction
    public GameObject congrats;

    //Array holding all of the sprites which could possibly be rendered onto the backgroundAsteroid GameObject
    public Sprite[] asteroids;

    //Float signifying the smallest possible transform scale of the GameObject
    public float smallestSize = 1f;
    //Float signifying the largest possible transform scale of the GameObject
    public float largestSize = 1.6f;

    //Holds a reference to the GameControllerScript of the GameController GameObject
    private GameControllerScript gameController;

    //Destroys BackgroundAsteroid GameObject when it is no longer visible to the player
    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    //Destroys BackGround asteroid GameObject and plays appropriate particles depending on sprite (either asteroid or satellite sprites), and displahys congrats message 1/5th of times
    public void DestroyAsteroid()
    {
        if(randSpriteInt >= 5)
        {
            Instantiate(satelliteParticles, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(asteroidParticles, transform.position, Quaternion.identity);
        }

        if(Random.Range(0, 5) == 1)
        {
            Instantiate(congrats, transform.position, Quaternion.identity);
        }
        
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    //Rotates GameObject to point towards specified point in world space
    private void RotateGameObject(Vector3 target, float RotationSpeed, float offset)
    {
        Vector3 dir = target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RotationSpeed * Time.deltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Generates random integer signifying random sprite to display on BackgroundAsteroid GameObject
        randSpriteInt = Random.Range(0, asteroids.Length);
        //Gets the SpriteRender component of this GameObject in order to display the specified random sprite on it
        GetComponent<SpriteRenderer>().sprite = asteroids[randSpriteInt];

        Vector3 randLoc;

        //Directs the target of the asteroid's motion depending on whether it was spawned from the left or right sides of the screen
        if (transform.position.x > 0)
        {
            randLoc = new Vector3(-13f, Random.Range(-7, 7), 0);
        }
        else
        {
            randLoc = new Vector3(13f, Random.Range(-7, 7), 0);
        }

        //Gets a reference to the RigidBody2D component of this GameObject and gives it a random angular velocity
        asteroidRigidBody = GetComponent<Rigidbody2D>();
        asteroidRigidBody.angularVelocity = Random.Range(-200, 200);

        //Creates new vector pointing from specified target position to the position of this GameObject
        Vector3 movementDir = randLoc - transform.position;

        //Rotates GameObject to face towards destination
        RotateGameObject(randLoc, 100, 0);
        asteroidRigidBody.velocity = movementDir.normalized * Random.Range(3f, 7f);

        //Generates a random float from smallestSize to largestSize and sets the scale of the transform to that random float
        float randomSize = Random.Range(smallestSize, largestSize);
        Vector3 randomAsteroidSize = new Vector3(randomSize, randomSize, 0f);
        gameObject.transform.localScale = randomAsteroidSize;
    }
}
