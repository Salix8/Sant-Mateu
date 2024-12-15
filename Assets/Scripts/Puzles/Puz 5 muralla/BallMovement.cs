using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float upwardForce = 5f;
    private Muralla murallaS;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float angleInRadians = transform.eulerAngles.z * Mathf.Deg2Rad + Random.Range(0.0f, 0.8f);
            Vector2 forceDirection = new Vector2(-Mathf.Cos(angleInRadians)- Random.Range(0.2f, 0.8f), Mathf.Sin(angleInRadians));

            Vector2 force = forceDirection * speed + Vector2.up * upwardForce;
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

    public void SetMuralla(Muralla muro)
    {
        murallaS = muro;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.collider.CompareTag("Shield"))
        {
            rb.velocity = new Vector2(rb.velocity.x, -Mathf.Abs(rb.velocity.y)).normalized * speed;
        }
        else if (collision.collider.CompareTag("Muralla"))
        {
            murallaS.SetVidaMuralla(-1);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible() // Este método se llama automáticamente cuando el objeto sale de la cámara.
    {
        Destroy(gameObject);
    }



    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.collider.CompareTag("Shield"))
    //     {
    //         // Calcula el ángulo de rebote usando la normal de colisión.
    //         Vector2 reflectDirection = Vector2.Reflect(rb.velocity, collision.contacts[0].normal);

    //         // Ajusta la velocidad manteniendo la magnitud constante.
    //         rb.velocity = reflectDirection.normalized * speed;

    //         // Opción: Agregar algo de fuerza adicional para dinamismo.
    //         rb.AddForce(Vector2.up * 0.5f, ForceMode2D.Impulse);
    //     }
    // }
}
