using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer : Agent
{
    protected override void CalculateSteeringForces()
    {
        Wander();
        //Separate(1, AgentManager.Instance.wanderers);
        StayInBounds();
    }
}
