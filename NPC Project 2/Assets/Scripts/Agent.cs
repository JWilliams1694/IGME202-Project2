using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PhysicsObject))]
public abstract class Agent : MonoBehaviour
{
    public PhysicsObject physicsObject;

    public float maxSpeed = 5f;
    public float maxForce = 5f;

    private Vector3 totalForce = Vector3.zero;

    [SerializeField]
    private float wanderAngle = 0f;
    public float maxWanderAngle = 45f;

    public float maxWanderChangePerSecond = 10f;

    public float visionRange = 3f;

    [SerializeField]
    Vector3 wanderTarget;

    [SerializeField]
    Vector3 futurePosition;

    protected SpriteRenderer sr;

    private void Awake()
    {
        physicsObject = GetComponent<PhysicsObject>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CalculateSteeringForces();

        totalForce = Vector3.ClampMagnitude(totalForce, maxForce);
        physicsObject.ApplyForce(totalForce);

        totalForce = Vector3.zero;

    }

    protected abstract void CalculateSteeringForces();

    protected Vector3 Seek(Vector3 targetPosition, float weight = 1f)
    {
        //calculate desired velocity
        Vector3 desiredVelocity = targetPosition - physicsObject.Position;

        //set desired velocity magnitude to max speed
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        //calculate the seek steering force
        Vector3 seekingForce = desiredVelocity - physicsObject.Velocity;

        //apply the seek steering force
        totalForce += seekingForce * weight;

        return seekingForce;
    }
    protected Vector3 Flee(Vector3 targetPosition, float weight = 1f)
    {
        //calculate desired velocity
        Vector3 desiredVelocity = physicsObject.Position - targetPosition;

        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        // calculate the flee steering force
        Vector3 fleeingForce = desiredVelocity - physicsObject.Velocity;

        //apply the flee steering force
        totalForce += fleeingForce * weight;

        return fleeingForce;
    }

    protected Vector3 Wander(float weight = 1f)
    {
        //update angle of our current wander
        float maxWanderChange = maxWanderChangePerSecond * Time.deltaTime;

        wanderAngle += Random.Range(-maxWanderChange, maxWanderChange);

        wanderAngle = Mathf.Clamp(wanderAngle, -maxWanderAngle, maxWanderAngle);

        //get position that is defined by the wander angle
        wanderTarget = Quaternion.Euler(0, 0, wanderAngle) * physicsObject.Direction.normalized + physicsObject.Position;

        //seek towards wander position
        return Seek(wanderTarget);
    }

    protected void Separate<T>(float personalSpace, List<T> agents) where T : Agent
    {
        float sqrPersonalSpace = Mathf.Pow(personalSpace, 2);

        foreach (T other in agents)
        {
            float sqrDist = Vector3.SqrMagnitude(other.physicsObject.Position - physicsObject.Position);

            if (sqrDist < float.Epsilon)
            {
                continue;
            }

            if (sqrDist < sqrPersonalSpace)
            {
                float weight = sqrPersonalSpace / (sqrDist + .1f);
                Flee(other.physicsObject.Position, weight);
            }
        }
    }


    protected void StayInBounds(float weight = 1f)
    {
        futurePosition = GetFuturePosition();
        if (futurePosition.x > AgentManager.Instance.cameraSize.x - AgentManager.Instance.cameraEdge.x ||
            futurePosition.x < -AgentManager.Instance.cameraSize.x + AgentManager.Instance.cameraEdge.x ||
            futurePosition.y > AgentManager.Instance.cameraSize.y - AgentManager.Instance.cameraEdge.y-2 ||
            futurePosition.y < -AgentManager.Instance.cameraSize.y + AgentManager.Instance.cameraEdge.y)
        {
            Seek(Vector3.zero, weight);
        }
    }

    public Vector3 GetFuturePosition(float timeToLookAhead = 1f)
    {
        return physicsObject.Position + physicsObject.Velocity * timeToLookAhead;
    }

    protected void AvoidObstacle(Obstacle obstacle)
    {
        //get vector from agent to obstacle
        Vector3 toObstacle = obstacle.position - physicsObject.Position;

        //check if obstacle is behind
        float fwdToObstacleDot = Vector3.Dot(physicsObject.Direction, toObstacle);
        if (fwdToObstacleDot < 0)
        {
            return;
        }

        //check if position is too far left or right
        float rightToObstacleDot = Vector3.Dot(physicsObject.Right, toObstacle);
        if (Mathf.Abs(rightToObstacleDot) > physicsObject.radius + obstacle.radius)
        {
            return;
        }

        //check if obstacle is within range
        if (fwdToObstacleDot > visionRange)
        {
            return;
        }

        //avoid obstacle
        Vector3 desiredVelocity;

        if (rightToObstacleDot > 0)
        {
            //steer left
            desiredVelocity = physicsObject.Right * -maxSpeed;
        }
        else
        {
            //steer right
            desiredVelocity = physicsObject.Right * maxSpeed;
        }

        //create weight depending how close
        float weight = visionRange / (fwdToObstacleDot + .1f);

        //calculate steering force
        Vector3 steeringForce = desiredVelocity - physicsObject.Velocity * weight;
        totalForce += steeringForce;
    }

    protected void AvoidAllObstacles()
    {
        foreach (Obstacle obstacle in ObstacleManager.Instance.Obstacles)
        {
            AvoidObstacle(obstacle);
        }
    }
}