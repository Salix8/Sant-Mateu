using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cesto_move : MonoBehaviour
{

    public int points = 0;
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Movement (float value){
        Vector2 position = rb2d.position;

        position.x = value/10;

        rb2d.MovePosition(position);

        
    }
}
