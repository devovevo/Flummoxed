using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brainScript : MonoBehaviour
{
    bool descending;
    public int descentSpeed;
    public int brainHealth;
    public int xSpeed;
    Rigidbody2D brainRigidBody;

    public int warningTime;

    public AudioClip warningSound;

    public GameObject greenProjectile;
    public GameObject laser;

    public GameObject redExplosion;
    public GameObject finalExplosion;

    GameControllerScript gameController;

    SpriteRenderer brainSpriteRenderer;

    public ParticleSystem laserParticles;
    public ParticleSystem brainParticles;
    public ParticleSystem brainDeathParticles;

    public GameObject congrats;

    public float shootSpeed;
    public float laserTime;

    AudioSource brainAudio;

    int stage;


    // Start is called before the first frame update
    void Start()
    {
        laserParticles = GetComponent<ParticleSystem>();

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameControllerScript>();

        brainAudio = GetComponent<AudioSource>();

        brainSpriteRenderer = GetComponent<SpriteRenderer>();

        stage = 1;

        brainRigidBody = GetComponent<Rigidbody2D>();
        brainRigidBody.velocity = new Vector2(0, -descentSpeed);

        descending = true;

        InvokeRepeating("ShootGreenProjectile", 2f, shootSpeed);
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void ShootGreenProjectile()
    {
        Instantiate(greenProjectile, gameObject.transform.position, Quaternion.identity);
    }

    void ShootLaser()
    {
        Vector3 equivalentPosition = new Vector3(transform.position.x, transform.position.y - 7.75f, 0);
        Instantiate(laser, equivalentPosition, Quaternion.identity);

        laserParticles.Pause();
        laserParticles.Clear();

        CancelInvoke("PlayWarningSound");
    }

    void ShowWarning()
    {
        laserParticles.Play();

        Invoke("ShootLaser", warningTime);
        InvokeRepeating("PlayWarningSound", 0, warningSound.length);
    }

    void PlayWarningSound()
    {
        brainAudio.PlayOneShot(warningSound);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 4 && descending == true)
        {
            brainRigidBody.velocity = new Vector2(xSpeed, 0);
            descending = false;
        }

        if (transform.position.x < -8.81)
        {
            brainRigidBody.velocity = new Vector2(xSpeed, 0);
        }   
        else if (transform.position.x > 8.81)
        {
            brainRigidBody.velocity = new Vector2(-xSpeed, 0);
        }

        if (brainHealth < 60 && stage == 1)
        {
            CancelInvoke();
            InvokeRepeating("ShowWarning", 1f, laserTime + warningTime);

            stage = 2;
        }
        else if (brainHealth < 40 && stage == 2)
        {
            CancelInvoke();

            InvokeRepeating("ShowWarning", 1f, laserTime + warningTime);
            InvokeRepeating("ShootGreenProjectile", 2f, shootSpeed);

            stage = 3;
        }

        if(gameController.level == 7 && stage != 0 && gameController.gamemode != "Endless")
        {
            brainRigidBody.velocity = new Vector2(0, descentSpeed);
            stage = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collidingObject)
    {
        if (collidingObject.name == "bullet(Clone)")
        {
            BrainDamage(collidingObject.gameObject);
        }
    }

    void BrainDamage(GameObject source)
    { 
        string name = source.name;

        if (name == "bullet(Clone)" || (name == "playership(Clone)" && !gameController.respawnedSingleRecently) || (name == "rightship(Clone)" && !gameController.respawnedRightRecently) || (name == "leftship(Clone)" && !gameController.respawnedLeftRecently))
        {
            if (name == "playership(Clone)")
            {
                brainHealth -= 5;
                brainParticles.Emit(10);
            }
            else
            {
                brainHealth--;
                brainParticles.Emit(2);
            }

            if (brainHealth > 0)
            {
                Instantiate(redExplosion, gameObject.transform.position, Quaternion.identity);

                if (Random.Range(0, 4) == 1)
                {
                    Instantiate(congrats, transform.position, Quaternion.identity);
                }

                if (name == "bullet(Clone)")
                {
                    source.SetActive(false);
                    Destroy(source);
                }

                gameController.incrementScore(20);

                if (brainHealth > 60)
                {
                    brainSpriteRenderer.color = new Color(brainSpriteRenderer.color.r, brainSpriteRenderer.color.g, brainSpriteRenderer.color.b - 0.01f, brainSpriteRenderer.color.a);
                }
                else if (brainHealth > 40)
                {
                    brainSpriteRenderer.color = new Color(brainSpriteRenderer.color.r, brainSpriteRenderer.color.g - 0.01f, brainSpriteRenderer.color.b, brainSpriteRenderer.color.a);
                }
                else
                {
                    brainSpriteRenderer.color = new Color(brainSpriteRenderer.color.r, brainSpriteRenderer.color.g - 0.01f, brainSpriteRenderer.color.b - 0.01f, brainSpriteRenderer.color.a);
                }
            }
            else
            {
                Vector3 bigExplosion = new Vector3(21, 21, 0f);
                Instantiate(redExplosion, gameObject.transform.position, Quaternion.identity).transform.localScale = bigExplosion;

                Instantiate(brainDeathParticles, transform.position, Quaternion.identity);
                
                for (int i = -3; i < 3; i++)
                {
                    Instantiate(congrats, new Vector3(transform.position.x - 8 * i, transform.position.y, 0), Quaternion.identity);
                }

                if (name == "bullet(Clone)")
                {
                    source.SetActive(false);
                    Destroy(source);
                }
                
                gameObject.SetActive(false);
                Destroy(gameObject);

                gameController.incrementScore(5000);
            }
        }
    }
}
