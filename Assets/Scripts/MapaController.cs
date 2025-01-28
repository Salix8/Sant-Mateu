using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapaController : MonoBehaviour
{
     public ProgressionManagerProxy progressionmanagerproxy;
    
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
        Debug.Log(progressionmanagerproxy.getzona());
        lugar[progressionmanagerproxy.getzona()].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
