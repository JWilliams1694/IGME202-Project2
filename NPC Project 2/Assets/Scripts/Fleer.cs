using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleer : Agent
{
    PhysicsObject spriteInfo1;
    PhysicsObject spriteInfo2;
    public List<GameObject> seekerList;
    public GameObject seeker;
    Vector3 position;
    public Vector3 cameraSize;
    protected override void CalculateSteeringForces()
    {
        Flee(seeker.transform.position);
    }
    protected override void Update()
    {
        base.Update();

        cameraSize.y = Camera.main.orthographicSize;
        cameraSize.x = cameraSize.y * Camera.main.aspect;
        //do a circle collision check to see if the seeker caught me
        //if caught, teleport to random position in world
        foreach (GameObject seeker in seekerList)
        {
            if (CircleCollision(gameObject, seeker)) 
        {
            position.x = Random.Range(-cameraSize.x,cameraSize.x);
            position.y = Random.Range(-cameraSize.y,cameraSize.y);
            transform.position = position;
        }
        }
        
    }
    public bool CircleCollision(GameObject obj1, GameObject obj2)
    {
        spriteInfo1 = obj1.GetComponent<PhysicsObject>();
        spriteInfo2 = obj2.GetComponent<PhysicsObject>();
        float distance = spriteInfo1.Position.x - spriteInfo2.Position.x;
        distance = Mathf.Pow(distance, 2);
        distance += Mathf.Pow(spriteInfo1.Position.y - spriteInfo2.Position.y, 2);
        distance = Mathf.Sqrt(distance);
        if ((obj1.GetComponent<PhysicsObject>().Radius + obj2.GetComponent<PhysicsObject>().Radius) > distance)
        {
            return true;
        }
        else return false;
    }
}
