using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redexplosionScript : MonoBehaviour
{
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        
    }
}