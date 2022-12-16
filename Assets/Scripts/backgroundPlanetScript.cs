using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundPlanetScript : MonoBehaviour
{
    public float smallestSize;
    public float largestSize;

    public float lowestSpeed;
    public float highestSpeed;

    SpriteRenderer planetRenderer;

    public Sprite[] planets;

    Rigidbody2D planetRigidBody;

    float randomSize;
    float currentSize;

    float colorRunningTotal = 0.001f;
    float xVelocity = 0;
    float relativeVelocity = 0;

    // Start is called before the first frame update
    void Start()
    {
        int randSpriteInt = Random.Range(0, planets.Length);
        GetComponent<SpriteRenderer>().sprite = planets[randSpriteInt];

        planetRigidBody = GetComponent<Rigidbody2D>();

        randomSize = Random.Range(smallestSize, largestSize);
        currentSize = randomSize;

        Vector3 randomPlanetSize = new Vector3(randomSize, randomSize, 0f);
        gameObject.transform.localScale = randomPlanetSize;

        relativeVelocity = ((highestSpeed - lowestSpeed) * (randomSize / largestSize)) + lowestSpeed;

        if (transform.position.x > 0)
        {
            xVelocity = 0.1f * (randomSize / largestSize) + transform.position.x * 0.015f + Time.timeSinceLevelLoad * 0.001f;
        }
        else
        {
            xVelocity = -0.1f * (randomSize / largestSize) + transform.position.x * 0.015f - Time.timeSinceLevelLoad * 0.001f;
        }

        planetRigidBody.velocity = new Vector2(xVelocity, -1 * relativeVelocity);

        float colorOffset = 0.3f + 0.5f * (randomSize / largestSize);

        planetRenderer = GetComponent<SpriteRenderer>();
        planetRenderer.color = new Color(colorOffset, colorOffset, colorOffset, planetRenderer.color.a);

        planetRenderer.sortingOrder = Mathf.RoundToInt(-200 + (198 * (randomSize / largestSize)));

        InvokeRepeating("planetGrow", 0f, 0.05f);
    }

    void planetGrow()
    {
        currentSize *= 1.001f;
        colorRunningTotal *= 1.0001f;

        Vector3 currentPlanetSize = new Vector3(currentSize, currentSize, 0f);
        gameObject.transform.localScale = currentPlanetSize;

        float colorOffset = 0.25f + 0.45f * (randomSize / largestSize) + colorRunningTotal;

        planetRenderer = GetComponent<SpriteRenderer>();
        planetRenderer.color = new Color(colorOffset, colorOffset, colorOffset, planetRenderer.color.a);

        planetRenderer.sortingOrder = Mathf.RoundToInt(-200 * (1 - (randomSize / largestSize))) - 5;

        relativeVelocity *= 1.0005f;
        xVelocity *= 1.0005f;

        planetRigidBody.velocity = new Vector2(xVelocity, -1 * relativeVelocity);
    }

    void OnBecameInvisible() 
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
