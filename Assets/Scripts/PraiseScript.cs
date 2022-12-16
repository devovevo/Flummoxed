using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PraiseScript : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    public string[] messages;
    public Color[] colors;
    public AudioClip[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        messageText.text = messages[Mathf.FloorToInt(Random.Range(0, messages.Length))];
        messageText.color = colors[Mathf.FloorToInt(Random.Range(0, colors.Length))];
        GetComponent<AudioSource>().PlayOneShot(sounds[Mathf.FloorToInt(Random.Range(0, sounds.Length))]);

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1f);

        Invoke("DeleteMessage", 1f);
    }

    void DeleteMessage()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
