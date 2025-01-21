using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cheats : MonoBehaviour
{
    public void QRCompletado()
    {
        ProgresionManager.GetInstance().SetComplete(0); //QR
    }

}
