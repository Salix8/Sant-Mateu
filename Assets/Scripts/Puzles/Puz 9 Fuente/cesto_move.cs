using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cesto_move : MonoBehaviour
{
    public int points = 0; // Puntos totales
    Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Movement(float value)
    {
        Vector2 position = rb2D.position;
        position.x = value / 10;
        rb2D.MovePosition(position);
    }
}
