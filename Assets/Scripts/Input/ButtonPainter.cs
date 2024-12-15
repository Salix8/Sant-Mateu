using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class ButtonPainter : MonoBehaviour
{
    private List<Vector2> points; 
    private LineRenderer line;
    public GameObject lienzo;
    private Vector3 previousPosition;
    public float minDistance = 0.1f;
    private bool switcher = true;

    public GameObject imagen;
    public GameObject linePrefab;

    Line activeLine;

    void Update(){

        if(!switcher){
            if (Input.GetMouseButtonDown(0)){
                GameObject newLine = Instantiate(linePrefab);
                newLine.transform.SetParent(imagen.transform);
                activeLine = newLine.GetComponent<Line>();
            }
            if (Input.GetMouseButtonUp(0))
            {
                activeLine = null;
            }

            if (activeLine != null)
            {
                Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                activeLine.UpdateLine(mousepos);
            }
        }
        
    }
    
    

    public void change ()
    {
        if(switcher){
            imagen.SetActive(true);
            switcher = false;
        }
        else{
            imagen.SetActive(false);
            switcher = true;
        }
    }
}


