using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cesto_move : MonoBehaviour
{

    public int points = 0;
    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Movement (float value){
        Vector2 position = rigidbody2D.position;

        position.x = value/10;

        rigidbody2D.MovePosition(position);

        
    }
}
