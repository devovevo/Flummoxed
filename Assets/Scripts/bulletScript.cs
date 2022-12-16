using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bulletScript : MonoBehaviour
{
    public int speed = 25;
    public GameObject explosion;

    private GameControllerScript gameController;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameControllerScript>();
        
        Rigidbody2D bulletRB = GetComponent<Rigidbody2D>();
        bulletRB.velocity = new Vector2(0, speed);
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collidingObject)
    { 
        string name = collidingObject.gameObject.name;

        if (name == "asteroid(Clone)")
        {
            Instantiate(explosion, collidingObject.transform.position, Quaternion.identity);

            if (collidingObject.gameObject != null && collidingObject.gameObject.activeInHierarchy)
            {
                collidingObject.SendMessage("DestroyAsteroid");
            }

            gameObject.SetActive(false);
            Destroy(gameObject);

            gameController.incrementScore(100);
        }
        else if (name == "backgroundAsteroid(Clone)")
        {
            Instantiate(explosion, collidingObject.transform.position, Quaternion.identity);

            if (collidingObject.gameObject != null && collidingObject.gameObject.activeInHierarchy)
            {
                collidingObject.SendMessage("DestroyAsteroid");
            }

            gameObject.SetActive(false);
            Destroy(gameObject);

            gameController.incrementScore(100);
        }
        else if (name == "watermelondebris(Clone)")
        {
            Instantiate(explosion, collidingObject.transform.position, Quaternion.identity);

            gameObject.SetActive(false);
            collidingObject.gameObject.SetActive(false);

            Destroy(collidingObject.gameObject);
            Destroy(gameObject);

            gameController.incrementScore(50);
        }
    }
}
