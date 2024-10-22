using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    //Variables data
    public float score, playerHealth;

    [SerializeField]
    Text scoreLabel;
    [SerializeField]
    Text finalScore;
    [SerializeField]
    Slider healthSlider;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject gameOver;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 4;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = player.GetComponent<Player>().health;
        score = player.GetComponent<Player>().score;
        //update score
        scoreLabel.text = "SCORE: " + score;
        finalScore.text = "FINAL SCORE: " + score;
        //update health
        healthSlider.value = playerHealth;
        if (playerHealth > 0)
        {
            gameOver.GetComponent<SpriteRenderer>().enabled = false;
            finalScore.enabled = false;
        }
        else 
        {
            gameOver.GetComponent<SpriteRenderer>().enabled = true;
            finalScore.enabled = true;
            Time.timeScale = 0;
        }
    }
}
