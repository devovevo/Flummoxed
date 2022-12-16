using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watermelonScript : MonoBehaviour
{
    Rigidbody2D watermelonRigidBody;

    public GameObject explosion;
    public GameObject congrats;

    public float downSpeed;
    public float debrisDownSpeed;
    public float maxDebrisXSpeed;
    public float minDebrisXSpeed;

    public GameObject watermelonDebris;

    private GameControllerScript gameController;

    public GameObject watermelonParticles;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameControllerScript>();

        watermelonRigidBody = GetComponent<Rigidbody2D>();
        watermelonRigidBody.velocity = new Vector2(0, -downSpeed);
    }

    public void PlayerDamage()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Instantiate(watermelonParticles, transform.position, Quaternion.identity);

        gameObject.SetActive(false);
        Destroy(gameObject);

        gameController.incrementScore(20);
    }

    void OnTriggerEnter2D(Collider2D collidingObject)
    { 
        string name = collidingObject.gameObject.name;

        if (name == "bullet(Clone)")
        {
            Instantiate(explosion, collidingObject.transform.position, Quaternion.identity);
            Instantiate(watermelonParticles, transform.position, Quaternion.identity);

            if(Random.Range(0, 3) == 1)
            {
                Instantiate(congrats, transform.position, Quaternion.identity);
            }
            
            GameObject debris1 = Instantiate(watermelonDebris, collidingObject.transform.position, Quaternion.identity);
            GameObject debris2 = Instantiate(watermelonDebris, collidingObject.transform.position, Quaternion.identity);

            debris1.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(minDebrisXSpeed, maxDebrisXSpeed), -debrisDownSpeed);
            debris2.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-minDebrisXSpeed, -maxDebrisXSpeed), -debrisDownSpeed);

            gameObject.SetActive(false);
            collidingObject.gameObject.SetActive(false);

            Destroy(collidingObject.gameObject);
            Destroy(gameObject);

            gameController.incrementScore(20);
        }
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
