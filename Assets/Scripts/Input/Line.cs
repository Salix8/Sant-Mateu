using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    List<Vector2> points; 
    public LineRenderer LineRenderer;

    void SetPoint(Vector2 point){
        points.Add(point);

        LineRenderer.positionCount = points.Count;
        LineRenderer.SetPosition(points.Count -1, point);
    }

    public void UpdateLine(Vector2 position){
        if(points == null){
            points = new List<Vector2>();
            SetPoint(position);
            return;
        }
        if (Vector2.Distance(points[^1], position)>.1f){
            SetPoint(position);
        }
    }
}
