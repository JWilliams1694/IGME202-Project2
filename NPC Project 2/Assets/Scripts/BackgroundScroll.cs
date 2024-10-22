using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private float scrollSpeed = -.6f;
    Vector3 starting;
    [SerializeField]
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        starting = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Player>().health > 0)
        {
            float newPos = Mathf.Repeat(Time.time * scrollSpeed, 30);
            transform.position = starting + Vector3.up * newPos;
        }
        
    }
}
