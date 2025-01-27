using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapaController : MonoBehaviour
{
    public ProgresionManager progresionmanager;
    
    public GameObject[] lugar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake(){
        Debug.Log("Awake del mapa");

        foreach(GameObject obj in lugar){
            obj.SetActive(false);
        }
        Debug.Log(progresionmanager.zonactual);
        lugar[progresionmanager.zonactual].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
