using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftshipscript : MonoBehaviour
{
    public float speed = 20f;
    public GameObject bullet;
    AudioSource spaceshipAudioSource;

    public GameObject explosion;
    private GameControllerScript gameController;

    public float respawnSafeTime = 1.5f;
    bool respawnSafe;

    Renderer playerShipRenderer;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public GameObject playerDebris;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameControllerScript>();

        playerShipRenderer = gameObject.GetComponent<Renderer>();
        ShowShip();

        respawnSafe = true;
        spaceshipAudioSource = GetComponent<AudioSource>();

        Invoke("EndSafeTime", respawnSafeTime);

        InvokeRepeating("HideShip", 0f, 0.25f);
        InvokeRepeating("ShowShip", 0.15f, 0.25f);
    }

    void ShowShip()
    {
        playerShipRenderer.enabled = true;
    }

    void HideShip()
    {
        playerShipRenderer.enabled = false;
    }

    void EndSafeTime()
    {
        gameController.respawnedLeftRecently = false;

        CancelInvoke();
        ShowShip();

        respawnSafe = false;
    }

    // Update is called once per frame
    void Update()
    {
       if (transform.position.x < maxX && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else if (transform.position.x > minX && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }

        if (transform.position.y < maxY && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        else if (transform.position.y > minY && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter) && respawnSafe == false))
        {
            spaceshipAudioSource.PlayOneShot(spaceshipAudioSource.clip);

            Vector3 spawnBulletLoc = new Vector3(transform.position.x - 0.15f, transform.position.y, transform.position.z);
            Instantiate(bullet, spawnBulletLoc, Quaternion.identity).GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 1f);
        }
    }

    void OnTriggerEnter2D(Collider2D collidingObject)
    {
        string name = collidingObject.gameObject.name;

        if (name == "asteroid(Clone)" && respawnSafe == false)
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Instantiate(playerDebris, transform.position, Quaternion.identity);

            if (collidingObject.gameObject != null && collidingObject.gameObject.activeInHierarchy)
            {
                collidingObject.SendMessage("DestroyAsteroid");
            }

            gameObject.SetActive(false);
            Destroy(gameObject);

            gameController.leftShipDied();
        }
        else if (name == "backgroundAsteroid(Clone)" && respawnSafe == false)
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Instantiate(playerDebris, transform.position, Quaternion.identity);

            if (collidingObject.gameObject != null && collidingObject.gameObject.activeInHierarchy)
            {
                collidingObject.SendMessage("DestroyAsteroid");
            }

            gameObject.SetActive(false);
            Destroy(gameObject);

            gameController.leftShipDied();
        }
        else if (name == "watermelon(Clone)" && respawnSafe == false)
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Instantiate(playerDebris, transform.position, Quaternion.identity);

            if (collidingObject.gameObject != null && collidingObject.gameObject.activeInHierarchy)
            {
                collidingObject.SendMessage("PlayerDamage");
            }

            gameObject.SetActive(false);
            Destroy(gameObject);

            gameController.leftShipDied();    
        }
        else if (name == "watermelondebris(Clone)" && respawnSafe == false)
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Instantiate(playerDebris, transform.position, Quaternion.identity);

            gameObject.SetActive(false);
            collidingObject.gameObject.SetActive(false);

            Destroy(collidingObject.gameObject);
            Destroy(gameObject);

            gameController.leftShipDied();    
        }
        else if (name == "ufo(Clone)" && respawnSafe == false)
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Instantiate(playerDebris, transform.position, Quaternion.identity);

            if (collidingObject.gameObject != null && collidingObject.gameObject.activeInHierarchy)
            {
                collidingObject.SendMessage("PlayerDamage");
            }

            gameObject.SetActive(false);
            Destroy(gameObject);

            gameController.leftShipDied();   

        }
        else if (name == "laser(Clone)" && respawnSafe == false)
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Instantiate(playerDebris, transform.position, Quaternion.identity);

            gameObject.SetActive(false);
            Destroy(gameObject);

            gameController.leftShipDied();
        }
        else if (name == "greenenergyball(Clone)" && respawnSafe == false)
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Instantiate(playerDebris, transform.position, Quaternion.identity);

            gameObject.SetActive(false);
            collidingObject.gameObject.SetActive(false);

            Destroy(collidingObject.gameObject);
            Destroy(gameObject);

            gameController.leftShipDied();   
        }
        else if (name == "brain(Clone)" && respawnSafe == false)
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Instantiate(playerDebris, transform.position, Quaternion.identity);

            if (collidingObject.gameObject != null && collidingObject.gameObject.activeInHierarchy)
            {
                collidingObject.SendMessage("BrainDamage", gameObject);
            }
            
            gameObject.SetActive(false);
            Destroy(gameObject);

            gameController.leftShipDied();
        }
        else if (name == "Comet(Clone)" && respawnSafe == false)
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Instantiate(playerDebris, transform.position, Quaternion.identity);

            gameObject.SetActive(false);
            collidingObject.gameObject.SetActive(false);

            Destroy(collidingObject.gameObject);
            Destroy(gameObject);

            gameController.leftShipDied();
        }
    }
}
