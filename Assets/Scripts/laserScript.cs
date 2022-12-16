using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserScript : MonoBehaviour
{
    public float laserTime;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyLaser", laserTime);
    }

    void DestroyLaser()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
