using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destinationPlanetScript : MonoBehaviour
{
    public float initialY;
    public float initialX;

    SpriteRenderer planetRenderer;

    Rigidbody2D planetRigidBody;

    Vector3 planetSize;
    public float initialYVelocity;

    float currentYVelocity;

    private GameControllerScript gameController;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameControllerScript>();

        if (gameController.gamemode.Equals("Endless"))
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            
            planetRenderer = GetComponent<SpriteRenderer>();
            planetRigidBody = GetComponent<Rigidbody2D>();

            transform.position.Set(initialX, initialY, 0);
            planetRenderer.color = new Color(0.01f, 0.01f, 0.01f, planetRenderer.color.a);

            planetSize = new Vector3(0.005f, 0.005f, 0f);
            gameObject.transform.localScale = planetSize;

            planetRigidBody.velocity = new Vector2(0, -1 * initialYVelocity);

            currentYVelocity = initialYVelocity;

            InvokeRepeating("Grow", 0f, 0.05f);
        }
    }

    void Grow()
    {
        currentYVelocity += 2.5f * Mathf.Pow(10, -7);
        planetRigidBody.velocity = new Vector2(0, -1 * currentYVelocity);

        planetRenderer.sortingOrder = Mathf.RoundToInt(-330 + (198 * (1 - (transform.position.y / initialY))));

        float newSize = 2f * Mathf.Pow((1 - (transform.position.y / initialY)), 7) + 0.02f;

        planetSize = new Vector3(newSize, newSize, 0f);
        gameObject.transform.localScale = planetSize;

        float newColor = 0.2f + Mathf.Pow((0.9f * (1 - (transform.position.y / initialY))), 2);
        planetRenderer.color = new Color(newColor, newColor, newColor, planetRenderer.color.a);
    }
}
