using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Agent
{
    public List<Sprite> spriteList = new List<Sprite>();
    float timer;
    protected override void CalculateSteeringForces()
    {
        timer += Time.deltaTime;
        AvoidAllObstacles();
        StayInBounds(8f);
        if (AgentManager.Instance.player.GetComponent<SpriteRenderer>().color == Color.red)
        {
            sr.color = Color.yellow;
            Seek(AgentManager.Instance.player.transform.position, 5f);
        }
        else if (Vector3.Distance(AgentManager.Instance.player.transform.position, gameObject.transform.position) < 5)
        {
            sr.color = Color.red;
            Flee(AgentManager.Instance.player.transform.position, 5f);
        }
        else
        {
            sr.color = Color.white;
            Wander();
            Separate(7, AgentManager.Instance.stars);
            
        }
        if (timer > .3f)
        {
            sr.sprite = spriteList[Random.Range(0, 5)];
            timer = 0;
        }
        
    }
}
