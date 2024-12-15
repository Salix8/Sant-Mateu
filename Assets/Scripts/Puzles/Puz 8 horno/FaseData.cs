using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhaseData
{
    public List<Vector3> objectPositions; // Posiciones de los GameObjects
    public List<Quaternion> objectRotations; // Rotaciones de los GameObjects
    public int winningCount1; // Nuevo: Cantidad de objetos del grupo 1
    public int winningCount2; // Nuevo: Cantidad de objetos del grupo 2
}
