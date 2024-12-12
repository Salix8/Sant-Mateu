using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cesto_move : MonoBehaviour
{

    public int points = 0;
    Rigidbody2D rd2d;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Movement (float value){
        Vector2 position = rd2d.position;

        position.x = value/10;

        rd2d.MovePosition(position);

        
    }
}
