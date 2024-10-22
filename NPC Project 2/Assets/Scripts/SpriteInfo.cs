using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteInfo : MonoBehaviour
{

    //fields
    SpriteRenderer spriteRenderer;

    float minX;
    float minY;
    float maxX;
    float maxY;
    float radius;
    Vector3 size;
    public float MinX
    {
        get { return minX; }
    }
    public float MinY
    {
        get { return minY; }
    }
    public float MaxX
    {
        get { return maxX; }
    }
    public float MaxY
    {
        get { return maxY; }
    }
    public float Radius
    {
        get { return radius; }
    }  
    public Vector3 Position
    {
        get { return transform.position; }
    }    
    public Vector3 Size
    {
        get { return size; }
    }   
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Init();
    }

    void Init()
    {
        //Vector3 radiusVector = spriteRenderer.bounds.max - spriteRenderer.bounds.center;
        //radius = radiusVector.magnitude;
        
    }
    // Update is called once per frame
    void Update()
    {
        minX = spriteRenderer.bounds.min.x;
        minY = spriteRenderer.bounds.min.y;
        maxX = spriteRenderer.bounds.max.x;
        maxY = spriteRenderer.bounds.max.y;
        size = spriteRenderer.bounds.size;
        radius = spriteRenderer.bounds.max.y - spriteRenderer.bounds.center.y;
        radius = radius * .77f;
    }
   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, size);
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
