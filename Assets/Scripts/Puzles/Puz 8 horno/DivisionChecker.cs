using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivisionChecker : MonoBehaviour
{
    public List<GameObject> gameObjects; // Lista de GameObjects que representan las hogazas de pan
    public int winningCount1 = 1; // Número de objetos que deberían estar en el primer lado (tanda de 1)
    public int winningCount2 = 2; // Número de objetos que deberían estar en el segundo lado (tanda de 2)

    public LineDrawer lineDrawer; // Referencia al script que dibuja la línea

    public GamePhaseManager phaseManager;

    public void CheckDivisions()
    {
        int countSide1 = 0; // Contador para el lado 1
        int countSide2 = 0; // Contador para el lado 2
        bool isAnyObjectTouchingLine = false; // Verificar si algún objeto toca la línea

        foreach (GameObject obj in gameObjects)
        {
            if (IsTouchingLine(obj)) // Nuevo método para verificar si toca la línea
            {
                isAnyObjectTouchingLine = true;
                Debug.Log($"El objeto {obj.name} está tocando la línea.");
            }
            else if (IsInDivision(obj)) // Verificar el lado
            {
                countSide1++;
            }
            else
            {
                countSide2++;
            }
        }

        // Si algún objeto toca la línea, es derrota inmediata
        if (isAnyObjectTouchingLine)
        {
            Debug.Log("Inténtalo de nuevo. Hay objetos tocando la línea.");
            lineDrawer.ClearLine();
            return;
        }

        // Comprobar si la división cumple los criterios de victoria
        if ((countSide1 == winningCount1 && countSide2 == winningCount2) ||
            (countSide1 == winningCount2 && countSide2 == winningCount1))
        {
            Debug.Log("¡Has ganado esta fase!");
            lineDrawer.ClearLine();
            phaseManager.LoadNextPhase();
        }
        else
        {
            Debug.Log("Inténtalo de nuevo.");
            lineDrawer.ClearLine();
        }
    }

    // Nuevo método para verificar si un objeto está tocando la línea
    private bool IsTouchingLine(GameObject obj)
    {
        Vector3 lineStart = lineDrawer.startButton.transform.position;
        Vector3 lineEnd = lineDrawer.endButton.transform.position;

        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError($"El objeto {obj.name} no tiene un SpriteRenderer.");
            return false;
        }

        Bounds bounds = spriteRenderer.bounds;
        Vector3 objPosition = bounds.center; // Centro del sprite
        float margin = Mathf.Max(bounds.extents.x, bounds.extents.y); // Margen basado en el tamaño del sprite

        // Calcular la distancia perpendicular desde el objeto a la línea
        float distanceToLine = Mathf.Abs((lineEnd.x - lineStart.x) * (lineStart.y - objPosition.y) -
                                        (lineStart.x - objPosition.x) * (lineEnd.y - lineStart.y)) /
                            Vector3.Distance(lineStart, lineEnd);

        return distanceToLine <= margin; // Está tocando si la distancia es menor o igual al margen
    }


    // Lógica para determinar si el objeto está en el lado 1 de la línea
    private bool IsInDivision(GameObject obj)
    {
        // Obtener posiciones de inicio y fin de la línea
        Vector3 lineStart = lineDrawer.startButton.transform.position;
        Vector3 lineEnd = lineDrawer.endButton.transform.position;

        // Obtener el SpriteRenderer del objeto
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError($"El objeto {obj.name} no tiene un SpriteRenderer.");
            return false;
        }

        // Obtener el centro y dimensiones del sprite
        Bounds bounds = spriteRenderer.bounds;
        Vector3 objPosition = bounds.center; // Centro del sprite
        float margin = Mathf.Max(bounds.extents.x, bounds.extents.y); // Margen basado en el tamaño del sprite

        // Registrar datos en consola para depuración
        Debug.Log($"Objeto: {obj.name}, Centro: {objPosition}, Margen: {margin}");
        Debug.Log($"Línea: Start({lineStart}), End({lineEnd})");

        // Calcular la distancia perpendicular desde el objeto a la línea
        float distanceToLine = Mathf.Abs((lineEnd.x - lineStart.x) * (lineStart.y - objPosition.y) -
                                        (lineStart.x - objPosition.x) * (lineEnd.y - lineStart.y)) /
                            Vector3.Distance(lineStart, lineEnd);

        Debug.Log($"Distancia a la línea: {distanceToLine}");

        // Si la distancia es menor o igual al margen, el objeto está tocando la línea
        if (distanceToLine <= margin)
        {
            Debug.Log($"El objeto {obj.name} está tocando la línea.");
            return false; // Opcional: considerar los objetos en la línea como "fuera de cualquier lado"
        }

        // Calcular el producto cruzado para determinar el lado
        float crossProduct = (lineEnd.x - lineStart.x) * (objPosition.y - lineStart.y) -
                            (lineEnd.y - lineStart.y) * (objPosition.x - lineStart.x);

        Debug.Log($"CrossProduct: {crossProduct}");

        // Determinar el lado del objeto según el producto cruzado
        return crossProduct > 0;
    }

}
