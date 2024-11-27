using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutodestructorMouse : MonoBehaviour
{
    
    public void OnMouseDown()
    {
        // Destroy the gameObject after clicking on it
        Debug.Log("Se ha destruido el GameObject: " + this.gameObject.name);
        Destroy(this.gameObject);
    }

    private void OnMouseEnter()
    {
        Debug.Log("El raton ha entrado en " + this.gameObject.name);
    }

}
