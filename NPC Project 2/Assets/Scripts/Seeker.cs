using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : Agent
{
    public GameObject fleer;
    protected override void CalculateSteeringForces()
    {
        Seek(fleer.transform.position);
    }
}
