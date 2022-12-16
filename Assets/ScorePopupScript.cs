using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePopupScript : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    public Color[] colors;

    // Start is called before the first frame update
    void Start()
    {
        //messageText.color = colors[Mathf.FloorToInt(Random.Range(0, colors.Length))];

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1f);

        Invoke("DeleteMessage", 1f);
    }

    void DeleteMessage()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
