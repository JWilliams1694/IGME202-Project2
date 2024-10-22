using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private Vector3 velocity;
    private Vector3 acceleration;
    private Vector3 direction;


    public float mass = 1f;

    public bool useGravity = false;
    public bool useFriction = false;
    public bool bounceOffWalls = false;
    public float frictionCoeff = 0.2f;
    public float radius=.5f;

    private Vector3 cameraSize;
    [SerializeField]
    private Vector3 cameraEdge;


    public Vector3 Velocity => velocity;
    public Vector3 Direction => direction;
    public Vector3 Position => transform.position;
    public float Radius => radius;
    public Vector3 CameraSize => cameraSize;
    public Vector3 CameraEdge => cameraEdge;
    public Vector3 Right => transform.right;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Vector3 radiusVector = spriteRenderer.bounds.max - spriteRenderer.bounds.center;
        radius = radiusVector.magnitude;

        direction = Random.insideUnitCircle.normalized;
    }

    // Update is called once per frame
    void Update()
    {
       // cameraSize = AgentManager.Instance.camSize;
       // cameraSize.y = Camera.main.orthographicSize;
       // cameraSize.x = cameraSize.y * Camera.main.aspect;

       // cameraEdge.x = cameraSize.x * screenEdgePercent;
        //cameraEdge.y = cameraSize.y * screenEdgePercent;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back, direction);
        }

        if (useGravity)
        {
            ApplyGravity(new Vector3(0, -9.81f, 0));
        }
        if (useFriction)
        {
            ApplyFriction(frictionCoeff);
        }
        //calculate new velocity based current acceleration of object
        velocity += acceleration * Time.deltaTime;

        //calculate the new position based on the velocity for frame
        transform.position += velocity * Time.deltaTime;

        if (velocity != Vector3.zero)
        {
            direction = velocity.normalized;
        }
        transform.position = Position;

        //zero out the acceleration
        acceleration = Vector3.zero;

        if (bounceOffWalls == true)
        {
            Bounce();
        }

       
    }

    /// <summary>
    /// Applies force to this object following Newton's second law
    /// </summary>
    /// <param name="force">Force vector will act on this object</param>
    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    /// <summary>
    /// Applies friction force to this object
    /// </summary>
    /// <param name="coeff">coeficcient of friction</param>
    private void ApplyFriction(float coeff)
    {
        Vector3 friction = velocity * -1;
        friction.Normalize();
        friction = friction * coeff;

        ApplyForce(friction);
    }

    /// <summary>
    /// Applies gravity to object
    /// </summary>
    /// <param name="gravityForce">Force of gravity</param>
    private void ApplyGravity(Vector3 gravityForce)
    {
        acceleration += gravityForce;
    }

    private void Bounce()
    {
        //if hit screen edge, bounce back
        if (transform.position.x > cameraSize.x && velocity.x > 0)
        {
            velocity.x *= -1f;
        }
        if (transform.position.x < -cameraSize.x && velocity.x < 0)
        {
            velocity.x *= -1f;
        }
        if (transform.position.y > cameraSize.y && velocity.y > 0)
        {
            velocity.y *= -1f;
        }
        if (transform.position.y < -cameraSize.y && velocity.y < 0)
        {
            velocity.y *= -1f;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
