using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TagState
{
    It,
    NotIt,
    Counting
}

public class TagPlayer : Agent
{
    private TagState currentState = TagState.NotIt;
    public TagState CurrentState => currentState;

    float timer;
    public float visionDistance = 4;

    public SpriteRenderer spriteRenderer;
    public Sprite itSprite;
    public Sprite notItSprite;
    public Sprite countingSprite;
    ParticleSystem ps;
    


    protected override void CalculateSteeringForces()
    {
       
    }


    private bool IsTouching(TagPlayer otherPlayer)
    {
        float sqrDistance = Vector3.SqrMagnitude(physicsObject.Position - otherPlayer.physicsObject.Position);

        float sqrRarii = Mathf.Pow(physicsObject.radius, 2) + Mathf.Pow(otherPlayer.physicsObject.radius, 2);

        return sqrDistance < sqrRarii;
    }

}
