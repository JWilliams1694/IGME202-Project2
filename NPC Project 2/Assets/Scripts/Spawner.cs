using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject seeker;
    [SerializeField]
    GameObject wanderer;
    [SerializeField]
    GameObject fleer;
    GameObject tempFleer;
    GameObject tempSeeker;
    GameObject tempWander;
    Vector3 randomPos;
    Vector3 camSize;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        camSize.y = Camera.main.orthographicSize;
        camSize.x = camSize.y * Camera.main.aspect;
        randomPos.x = Random.Range(-camSize.x, camSize.x);
        randomPos.y = Random.Range(-camSize.y, camSize.y);
        randomPos.z = 0;
        tempFleer =Instantiate(fleer,randomPos,Quaternion.identity);
        tempSeeker = Instantiate(seeker, randomPos, Quaternion.identity);
        tempSeeker.GetComponent<Seeker>().fleer = tempFleer;
        tempFleer.GetComponent<Fleer>().seekerList.Add(tempSeeker);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
            tempFleer.GetComponent<Fleer>().seeker = tempSeeker;
        
        randomPos.x = Random.Range(-camSize.x, camSize.x);
        randomPos.y = Random.Range(-camSize.y, camSize.y);
        if (timer >= 3) 
        {
          tempSeeker=  Instantiate(seeker, randomPos, Quaternion.identity);
            tempSeeker.GetComponent<Seeker>().fleer = tempFleer;
            tempFleer.GetComponent<Fleer>().seekerList.Add(tempSeeker);
            timer = 0;
           tempWander= Instantiate(wanderer, randomPos, Quaternion.identity);
        }
    }
}
