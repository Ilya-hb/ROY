using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : Entity { 
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("PositionPlayer") == 1)
            transform.position = new Vector2(PlayerPrefs.GetFloat("xPosition"), PlayerPrefs.GetFloat("yPosition"));
        else if (PlayerPrefs.GetInt("PositionPlayer") == 0)
            transform.position = new Vector2(7.11f, -0.271f);
        if (PlayerPrefs.GetInt("PositionPlayer") == 2)
            transform.position = new Vector2(PlayerPrefs.GetFloat("xPosition"), PlayerPrefs.GetFloat("yPosition"));
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            PlayerPrefs.SetInt("PositionPlayer", 1);
            PlayerPrefs.SetFloat("xPosition", transform.position.x);
            PlayerPrefs.SetFloat("yPosition", transform.position.y);
        }
      /*  if (collision.CompareTag("Checkpoint2"))
        {
            PlayerPrefs.SetInt("PositionPlayer", 1);
            PlayerPrefs.SetFloat("xPosition", transform.position.x);
            PlayerPrefs.SetFloat("yPosition", transform.position.y);
        }*/
    }
    public void Reset()
    {
        PlayerPrefs.SetInt("PositionPlayer", 0);
    }
}
