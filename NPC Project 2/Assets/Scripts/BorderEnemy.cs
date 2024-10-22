using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderEnemy : Agent
{
    protected override void CalculateSteeringForces()
    {

        AvoidAllObstacles();
        StayInBounds(8f);
        if (AgentManager.Instance.player.GetComponent<SpriteRenderer>().color == Color.red)
        {
            sr.color = Color.yellow;
            Flee(AgentManager.Instance.player.transform.position, 5f);
        }
        else if (Vector3.Distance(AgentManager.Instance.player.transform.position, gameObject.transform.position) < 7)
        {
            sr.color = Color.red;
            Seek(AgentManager.Instance.player.transform.position);
        }
        else
        {
            sr.color = Color.white;
            Wander();
            Separate(.8f, AgentManager.Instance.borders);


        }
    }
}
