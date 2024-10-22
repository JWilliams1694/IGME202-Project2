using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip backgroundMusic;
    [SerializeField]
    AudioClip loseMusic;
    bool play;
    bool toggle;
    [SerializeField]
    bool deathToggle;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        toggle = true;
        play = true;
        deathToggle = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (play == true && toggle == true)
        {
            audioSource.PlayOneShot(backgroundMusic,.7f);
            toggle = false;
        }
        //Check if you just set the toggle to false
        if (play == false && toggle == true)
        {
            toggle = false;
        }

        if (player.GetComponent<Player>().health <= 0 && deathToggle == false) 
        {
            audioSource.Stop();
            deathToggle = true;
            audioSource.PlayOneShot(loseMusic, .7f);
        }
       
    }
}
