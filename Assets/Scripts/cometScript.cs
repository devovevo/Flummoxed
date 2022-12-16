using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cometScript : MonoBehaviour
{
    public float smallestSize;
    public float largestSize;

    public GameObject explosion;
    public GameObject congrats;

    private GameControllerScript gameController;

    Vector3 randomTarget;
    Rigidbody2D cometRigidBody;

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

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
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameControllerScript>();

        Vector3 randLoc;

        if (transform.position.x > 0)
        {
            randLoc = new Vector3(-13f, Random.Range(-10, 10), 0);
        }
        else
        {
            randLoc = new Vector3(13f, Random.Range(-10, 10), 0);
        }

        cometRigidBody = GetComponent<Rigidbody2D>();
        randomTarget = randLoc;

        Vector3 movementDir = randomTarget - transform.position;

        RotateGameObject(randomTarget, 900, 0);
        cometRigidBody.velocity = movementDir.normalized * Random.Range(5f, 9f);

        float randomSize = Random.Range(smallestSize, largestSize);

        Vector3 randomAsteroidSize = new Vector3(randomSize, randomSize, 0f);
        gameObject.transform.localScale = randomAsteroidSize;
    }

    void OnTriggerEnter2D(Collider2D collidingObject)
    { 
        string name = collidingObject.gameObject.name;

        if (name == "bullet(Clone)")
        {
            Instantiate(explosion, collidingObject.transform.position, Quaternion.identity);
            
            if (Random.Range(0, 3) == 2)
            {
                Instantiate(congrats, collidingObject.transform.position, Quaternion.identity);
            }
            
            gameObject.SetActive(false);
            collidingObject.gameObject.SetActive(false);

            Destroy(collidingObject.gameObject);
            Destroy(gameObject);

            gameController.incrementScore(70);
        }
    }
}
