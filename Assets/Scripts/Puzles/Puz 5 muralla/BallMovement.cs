using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float upwardForce = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float angleInRadians = transform.eulerAngles.z * Mathf.Deg2Rad;
            Vector2 forceDirection = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

            Vector2 force = forceDirection * speed + Vector2.up * upwardForce;
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}
