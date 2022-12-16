using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionScript : MonoBehaviour
{
    public float animationTime = 1f;
    AudioSource explosionAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        explosionAudioSource = GetComponent<AudioSource>();
        explosionAudioSource.PlayOneShot(explosionAudioSource.clip);

        Invoke("DestroyExplosion", animationTime);
    }
    
    void DestroyExplosion()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
