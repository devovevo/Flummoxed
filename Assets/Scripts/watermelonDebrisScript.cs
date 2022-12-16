using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watermelonDebrisScript : MonoBehaviour
{   
    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
