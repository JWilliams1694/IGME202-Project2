using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float radius = .5f;
    Vector3 spin;
    public Vector3 position => transform.position;

    private void Update()
    {
        spin.z = 10f * Time.deltaTime;
        transform.Rotate(spin);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(position, radius);
    }
}
