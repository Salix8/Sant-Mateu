using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flor_cont : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    public int puntos;
    // Start is called before the first frame update
    void Start()
    {
        
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = GetComponent<Rigidbody2D>().position;

    
        if(position.y < -8)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        cesto_move cesto  = other.collider.GetComponent<cesto_move >();

        cesto.points+=puntos;
        Debug.Log("Puntos: " + cesto.points);

        Destroy(gameObject);   
    }
}
