using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ufoScript : MonoBehaviour
{
    Rigidbody2D ufoRigidBody;
    public float xSpeed = 3f;
    public float ySpeed = 1.5f;

    public int health;
    public GameObject firstExplosion;
    public GameObject watermelon;
    public GameObject deathExplosion;

    public GameObject congrats;

    public float watermelonDropTime;

    private GameControllerScript gameController;

    SpriteRenderer ufoSpriteRenderer;

    ParticleSystem ufoParticles;
    public GameObject deathParticles;

    bool respawnSafe;

    // Start is called before the first frame update
    void Start()
    {
        ufoParticles = GetComponent<ParticleSystem>();

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameControllerScript>();

        ufoSpriteRenderer = GetComponent<SpriteRenderer>();

        float randInt = Random.Range(0, 1);

        int posNeg = -1;

        if (randInt >= 0.5)
        {
            posNeg *= -1;
        }

        ufoRigidBody = GetComponent<Rigidbody2D>();
        ufoRigidBody.velocity = new Vector2(xSpeed * posNeg, -ySpeed);

        InvokeRepeating("ufoUp", Random.Range(0, 2), Random.Range(2.5f, 4));
        InvokeRepeating("ufoDown", Random.Range(0, 2), Random.Range(0, 1.5f));

        InvokeRepeating("spawnWatermelon", 2f, watermelonDropTime);
    }

    void ufoUp()
    {
        ufoRigidBody.velocity = new Vector2(ufoRigidBody.velocity.x, ySpeed);
    }

    void ufoDown()
    {
        ufoRigidBody.velocity = new Vector2(ufoRigidBody.velocity.x, -ySpeed);
    }

    void spawnWatermelon()
    {
        Instantiate(watermelon, gameObject.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -8.5)
        {
            ufoRigidBody.velocity = new Vector2(xSpeed, -ySpeed);
        }
        else if (transform.position.x > 8.5)
        {
            ufoRigidBody.velocity = new Vector2(-xSpeed, -ySpeed);
        }

        if(transform.position.y < -9.19)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collidingObject)
    {
        DamageUfo(collidingObject.gameObject);
    }

    void PlayerDamage()
    {
        Instantiate(deathExplosion, transform.position, Quaternion.identity);
        Instantiate(deathParticles, transform.position, Quaternion.identity);

        gameObject.SetActive(false);
        Destroy(gameObject);

        gameController.incrementScore(300);
    }

    void DamageUfo(GameObject source)
    { 
        string name = source.name;

        if (name == "bullet(Clone)")
        {
            health--;

            ufoParticles.Emit(5);

            if (health > 0)
            {
                Instantiate(firstExplosion, gameObject.transform.position, Quaternion.identity);

                source.SetActive(false);
                Destroy(source);

                ufoSpriteRenderer.color = new Color(ufoSpriteRenderer.color.r, ufoSpriteRenderer.color.g - 0.15f, ufoSpriteRenderer.color.b - 0.15f, ufoSpriteRenderer.color.a);
            }
            else
            {
                Instantiate(deathExplosion, transform.position, Quaternion.identity);
                Instantiate(deathParticles, transform.position, Quaternion.identity);

                if (Random.Range(0, 2) == 1)
                {
                    Instantiate(congrats, transform.position, Quaternion.identity);
                }

                source.SetActive(false);
                gameObject.SetActive(false);

                Destroy(source);
                Destroy(gameObject);

                gameController.incrementScore(300);
            }
        }
    }
}
