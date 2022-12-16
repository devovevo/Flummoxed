using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundSpawnerScript : MonoBehaviour
{
    float y1, y2;
    public GameObject[] projectiles;

    // Start is called before the first frame update
    void Start()
    {
        Renderer spawnR = GetComponent<Renderer>();

        y1 = transform.position.y - spawnR.bounds.size.y/2;
        y2 = transform.position.y + spawnR.bounds.size.y/2;

        InvokeRepeating("spawnProjectile", Random.Range(0f, 5f), Random.Range(3f, 5f));
    }

    void spawnProjectile()
    {
        Vector2 spawnPoint = new Vector2(transform.position.x, Random.Range(y1, y2));

        int randSpawnInt = Random.Range(0, projectiles.Length);

        Instantiate(projectiles[randSpawnInt], spawnPoint, Quaternion.identity);
    }
}
