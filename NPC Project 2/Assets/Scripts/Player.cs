using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed=0f;
    Vector3 vehiclePosition = Vector3.zero;
    Vector3 direction = Vector3.zero;
    Vector3 velocity = Vector3.zero;
    float height;
    float width;

    public int health = 4;
    public int score;

    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        //starts vehicle where placed, not center
        vehiclePosition = transform.position;
        //sets camera size
        height = Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back, direction);
        }
        
        //velocity is  direction * speed * Time.deltaTime
        velocity = direction * speed * Time.deltaTime; 

        //add velocity to position
        vehiclePosition+= velocity;

        //checks for screen wrap
        ScreenWrap();

        //draw this vehicle at that position
        transform.position = vehiclePosition;
    }

    //method called by other script
    public void OnMove(InputAction.CallbackContext context)
    {
            direction = context.ReadValue<Vector2>();
    }
    public void ScreenWrap()
    {
        if (vehiclePosition.x > width)
        {
            vehiclePosition.x = width;
        }
        else if (vehiclePosition.x < -width)
        {
            vehiclePosition.x = -width;
        }
        if (vehiclePosition.y > height-2)
        {
            vehiclePosition.y = height-2;
        }
        else if (vehiclePosition.y < -height)
        {
            vehiclePosition.y = -height;
        }
    }

    public bool CircleCollision(Agent obj2)
    {
        SpriteInfo spriteInfo1 = GetComponent<SpriteInfo>();
        SpriteInfo spriteInfo2 = obj2.GetComponent<SpriteInfo>();
        float distance = spriteInfo1.Position.x - spriteInfo2.Position.x;
        distance = Mathf.Pow(distance, 2);
        distance += Mathf.Pow(spriteInfo1.Position.y - spriteInfo2.Position.y, 2);
        distance = Mathf.Sqrt(distance);
        if ((spriteInfo1.Radius + spriteInfo2.Radius) > distance)
        {
            return true;
        }
        else return false;
    }
}
