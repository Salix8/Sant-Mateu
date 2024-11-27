using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheats : MonoBehaviour
{
    public void QRCompletado()
    {
        ProgresionManager.GetInstance().SetComplete(0); //QR
    }
}
