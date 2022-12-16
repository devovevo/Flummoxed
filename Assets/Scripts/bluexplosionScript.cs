using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bluexplosionScript : MonoBehaviour
{
    public float animationTime = 1f;
    AudioSource bluexplosionAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        bluexplosionAudioSource = GetComponent<AudioSource>();
        bluexplosionAudioSource.PlayOneShot(bluexplosionAudioSource.clip);

        Invoke("DestroyExplosion", animationTime);
    }
    
    void DestroyExplosion()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
